using _0_Framework.Application.Extensions;
using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Models.ProductPicture;
using AutoMapper;
using DM.Domain.ProductDiscount;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Infrastructure.Persistence.Context;
using SM.Application.Contracts.ProductFeature.DTOs;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Helpers.Product;

public class ProductHelper : IProductHelper
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly IMongoHelper<ProductDiscount> _productDiscount;
    private readonly InventoryDbContext _inventoryContext;
    private readonly IMapper _mapper;
    private readonly IInventoryHelper _inventoryHelper;

    public ProductHelper(
        ShopDbContext shopContext, IMongoHelper<ProductDiscount> productDiscount,
        InventoryDbContext inventoryContext, IMapper mapper, IInventoryHelper inventoryHelper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _productDiscount = Guard.Against.Null(productDiscount, nameof(_productDiscount));
        _inventoryContext = Guard.Against.Null(inventoryContext, nameof(_inventoryContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
    }

    #endregion

    #region MapProductsFromProductCategories

    public async Task<ProductQueryModel> MapProductsFromProductCategories(SM.Domain.Product.Product product)
    {
        var mappedProduct = _mapper.Map(product, new ProductQueryModel
        {
            CategoryId = product.CategoryId.Value
        });

        return await MapProducts(mappedProduct);
    }

    #endregion

    #region MapProducts

    public async Task<T> MapProducts<T>(T product, bool hotDiscountQuery = false) where T : ProductQueryModel
    {
        #region all discounts query

        var discounts = (await _productDiscount.AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .ToListAsyncSafe()
            )
            .Select(x => new
            {
                x.ProductId,
                x.Rate
            }).ToList();

        if (hotDiscountQuery)
        {
            discounts = discounts.Where(x => x.Rate >= 25).ToList();
        }

        #endregion

        #region all categories query

        var categories = await _shopContext.ProductCategories.AsQueryable()
            .Select(x => new
            {
                x.Id,
                x.Title
            }).ToListAsync();

        #endregion

        product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId).Title.ToString();

        (bool existsProductInventory, double productUnitPrice, long currentCount) = GetProductInventory(product.Id).Result;

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
                var discountAmount = Math.Round((productUnitPrice * discountRate / 100));
                product.PriceWithDiscount = (productUnitPrice - discountAmount).ToMoney();
            }
        }


        return product;
    }

    #endregion

    #region Get Product UnitPrice

    public async Task<(bool, double, long)> GetProductInventory(long productId)
    {
        if (!(await _inventoryContext.Inventory.AnyAsync(x => x.ProductId == productId)))
            return (false, default, default);

        var inventory = await _inventoryContext.Inventory
            .Where(x => x.ProductId == productId)
            .Select(x => new
            {
                x.Id,
                x.UnitPrice,
            }).FirstOrDefaultAsync();

        var currentCount = await _inventoryHelper.CalculateCurrentCount(inventory.Id);

        return (true, inventory.UnitPrice, currentCount);
    }

    #endregion

    #region GetProductPictures

    public List<ProductPictureQueryModel> GetProductPictures(long productId)
    {
        var productPictures = _shopContext.ProductPictures.Where(x => x.ProductId == productId).ToList();

        if (productPictures is null)
            return new List<ProductPictureQueryModel>();

        return productPictures
            .Select(p => _mapper.Map(p, new ProductPictureQueryModel()))
            .ToList();
    }

    #endregion

    #region GetProductFeatures

    public List<ProductFeatureDto> GetProductFeatures(long productId)
    {
        var productFeatures = _shopContext.ProductFeatures.Where(x => x.ProductId == productId).ToList();

        if (productFeatures is null)
            return new List<ProductFeatureDto>();

        return productFeatures
            .Select(p => _mapper.Map(p, new ProductFeatureDto()))
            .ToList();
    }

    #endregion

    #region GetProductPriceById

    public double GetProductPriceById(long id)
    {
        var inventories = _inventoryContext.Inventory
           .Select(x => new
           {
               x.ProductId,
               x.InStock,
               x.UnitPrice
           }).ToList();

        var price = inventories.FirstOrDefault(x => x.ProductId == id).UnitPrice;

        return price;
    }

    #endregion
}