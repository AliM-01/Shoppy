using _0_Framework.Application.Extensions;
using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using AutoMapper;
using DM.Domain.ProductDiscount;
using IM.Application.Inventory.Helpers;
using IM.Domain.Inventory;
using MongoDB.Driver;
using SM.Application.Product.DTOs.Site;
using SM.Application.ProductFeature.DTOs;
using SM.Application.ProductPicture.DTOs;
using SM.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Infrastructure.Services;

public class ProductHelper : IProductHelper
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;
    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IRepository<ProductDiscount> _productDiscount;
    private readonly IRepository<Inventory> _inventoryContext;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public ProductHelper(IRepository<Domain.Product.Product> productRepository,
                         IRepository<ProductDiscount> productDiscount,
                         IRepository<Inventory> inventoryContext,
                         IRepository<Domain.ProductPicture.ProductPicture> productPictureRepository,
                         IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository,
                         IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository,
                         IMapper mapper,
                         IInventoryHelper inventoryHelper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _productDiscount = Guard.Against.Null(productDiscount, nameof(_productDiscount));
        _inventoryContext = Guard.Against.Null(inventoryContext, nameof(_inventoryContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #region MapProductsFromProductCategories

    public async Task<ProductSiteDto> MapProductsFromProductCategories(Domain.Product.Product product)
    {
        var mappedProduct = await MapProducts<ProductSiteDto>(product);

        mappedProduct.CategoryId = product.CategoryId;

        return mappedProduct;
    }

    #endregion

    #region MapProducts

    public async Task<T> MapProducts<T>(Domain.Product.Product req, bool hotDiscountQuery = false) where T : ProductSiteDto
    {
        var product = _mapper.Map(req, default(T));

        #region all discounts query

        var discounts = await _productDiscount.AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Select(x => new
            {
                x.ProductId,
                x.Rate
            }).ToListAsyncSafe();

        if (hotDiscountQuery)
            discounts = discounts.Where(x => x.Rate >= 25).ToList();

        #endregion

        product.Category = (await _productCategoryRepository.FindByIdAsync(product.CategoryId)).Title;

        (bool existsProductInventory, decimal productUnitPrice, long currentCount) = GetProductInventory(product.Id).Result;

        if (existsProductInventory)
        {
            // calculate unitPrice
            product.Price = productUnitPrice.ToMoney();
            product.UnitPrice = productUnitPrice;

            if (discounts.Any(x => x.ProductId == product.Id))
            {
                // calculate discountRate
                int discountRate = discounts.FirstOrDefault(x => x.ProductId == product.Id).Rate;
                product.DiscountRate = discountRate;
                product.HasDiscount = discountRate > 0;

                // calculate PriceWithDiscount
                decimal discountAmount = Math.Round(productUnitPrice * discountRate / 100);
                product.PriceWithDiscount = (productUnitPrice - discountAmount).ToMoney();
            }
        }


        return product;
    }

    #endregion

    #region Get Product UnitPrice

    public async Task<(bool, decimal, long)> GetProductInventory(string productId)
    {
        if (!await _inventoryContext.ExistsAsync(x => x.ProductId == productId))
            return (false, default, default);

        var filter = Builders<Inventory>.Filter.Eq(x => x.ProductId, productId);
        var inventory = await _inventoryContext.FindOne(filter);

        long currentCount = await _inventoryHelper.CalculateCurrentCount(inventory.Id);

        return (true, inventory.UnitPrice, currentCount);
    }

    #endregion

    #region GetProductPictures

    public List<ProductPictureSiteDto> GetProductPictures(string productId)
    {
        var productPictures = _productPictureRepository
            .AsQueryable()
            .Where(x => x.ProductId == productId).ToListSafe();

        if (productPictures is null)
            return new List<ProductPictureSiteDto>();

        return productPictures
            .Select(p => _mapper.Map(p, new ProductPictureSiteDto()))
            .ToList();
    }

    #endregion

    #region GetProductFeatures

    public List<ProductFeatureDto> GetProductFeatures(string productId)
    {
        var productFeatures = _productFeatureRepository
            .AsQueryable()
            .Where(x => x.ProductId == productId).ToListSafe();

        if (productFeatures is null)
            return new List<ProductFeatureDto>();

        return productFeatures
            .Select(p => _mapper.Map(p, new ProductFeatureDto()))
            .ToList();
    }

    #endregion

    #region GetProductPriceById

    public decimal GetProductPriceById(string id)
    {
        var inventories = _inventoryContext.AsQueryable()
           .Select(x => new
           {
               x.ProductId,
               x.InStock,
               x.UnitPrice
           }).ToListSafe();

        decimal price = inventories.FirstOrDefault(x => x.ProductId == id).UnitPrice;

        return price;
    }

    #endregion
}