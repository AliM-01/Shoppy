using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Models.Paging;
using _01_Shoppy.Query.Helpers.Product;
using AutoMapper;
using DM.Infrastructure.Persistence.Context;
using IM.Infrastructure.Persistence.Context;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Query;

public class ProductQuery : IProductQuery
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly DiscountDbContext _discountContext;
    private readonly IProductHelper _productHelper;
    private readonly InventoryDbContext _inventoryContext;
    private readonly IMapper _mapper;

    public ProductQuery(
        ShopDbContext shopContext, DiscountDbContext discountContext,
         InventoryDbContext inventoryContext, IProductHelper productHelper, IMapper mapper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _discountContext = Guard.Against.Null(discountContext, nameof(_discountContext));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _inventoryContext = Guard.Against.Null(inventoryContext, nameof(_discountContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    #region Get Latest Products

    public async Task<Response<List<ProductQueryModel>>> GetLatestProducts()
    {
        var latestProducts = await _shopContext.Products
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(product =>
                   _mapper.Map(product, new ProductQueryModel()))
               .ToListAsync();

        var returnData = new List<ProductQueryModel>();

        latestProducts.ForEach(p =>
        {
            var mappedProduct = _productHelper.MapProducts<ProductQueryModel>(p).Result;
            returnData.Add(mappedProduct);
        });

        return new Response<List<ProductQueryModel>>(returnData);
    }

    #endregion

    #region Get Hotest Discount Products

    public async Task<Response<List<ProductQueryModel>>> GetHotestDiscountProducts()
    {
        List<long> hotDiscountRateIds = await _discountContext.CustomerDiscounts.AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Where(x => x.Rate >= 25)
            .Take(8)
            .Select(x => x.ProductId).ToListAsync();

        var products = await _shopContext.Products
               .Include(x => x.Category)
               .Where(x => hotDiscountRateIds.Contains(x.Id))
               .OrderByDescending(x => x.LastUpdateDate)
               .Take(8)
               .Select(product =>
                   _mapper.Map(product, new ProductQueryModel()))
               .ToListAsync();

        var returnData = new List<ProductQueryModel>();

        products.ForEach(p =>
        {
            var mappedProduct = _productHelper.MapProducts<ProductQueryModel>(p, true).Result;
            returnData.Add(mappedProduct);
        });

        return new Response<List<ProductQueryModel>>(returnData);
    }

    #endregion

    #region Search

    public async Task<Response<SearchProductQueryModel>> Search(SearchProductQueryModel search)
    {
        var query = _shopContext.Products
               .OrderByDescending(x => x.LastUpdateDate)
               .AsQueryable();

        #region inventories query

        var inventories = await _inventoryContext.Inventory.AsQueryable()
            .Select(x => new
            {
                x.ProductId,
                x.InStock,
                x.UnitPrice
            }).ToListAsync();

        var inventoryIds = inventories.Select(x => x.ProductId).ToArray();

        query = query.Where(p => inventoryIds.Contains(p.Id));

        #endregion

        #region filter selected categories slugs

        if (search.SelectedCategories is not null && search.SelectedCategories.Any())
        {
            var selectedCategoriesId = new List<long>();

            foreach (var categorySlug in search.SelectedCategories)
            {
                if (await _shopContext.ProductCategories
                    .AnyAsync(x => x.Slug.Trim() == categorySlug.Trim()))
                {
                    selectedCategoriesId.Add(
                        _shopContext.ProductCategories.FirstOrDefault(x => x.Slug.Trim() == categorySlug.Trim()).Id);
                }
            }

            query = query.Where(x => selectedCategoriesId.Contains(x.CategoryId.Value));
        }

        #endregion

        #region filter

        #region filter price

        var maxPrice = inventories.OrderByDescending(p => p.UnitPrice).FirstOrDefault();
        search.FilterMaxPrice = maxPrice.UnitPrice;

        if (search.SelectedMaxPrice == 0)
            search.SelectedMaxPrice = maxPrice.UnitPrice;

        query = query.ToArray()
            .Where(p => GetProductPriceById(p.Id) >= search.SelectedMinPrice)
            .AsQueryable();

        query = query.ToArray()
            .Where(p => GetProductPriceById(p.Id) <= search.SelectedMaxPrice)
            .AsQueryable();

        #endregion

        #region filter phrase

        if (!string.IsNullOrEmpty(search.Phrase))
        {
            query = query.Where(s => s.Title.Contains(search.Phrase)
            || s.ShortDescription.Contains(search.Phrase));
        }

        #endregion

        #region filter sort date order

        switch (search.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate).AsQueryable();
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate).AsQueryable();
                break;
        }

        #endregion

        #endregion filter

        #region paging

        var pager = Pager.Build(search.PageId, query.ToList().Count,
            search.TakePage, search.ShownPages);
        var pagedEntities = query.Paging(pager).ToList();

        List<ProductQueryModel> allEntities = pagedEntities.ToList()
            .Select(product =>
                   _mapper.Map(product, new ProductQueryModel()))
            .ToList();

        var mappedProducts = new List<ProductQueryModel>();

        allEntities.ForEach(p =>
        {
            var mappedProduct = _productHelper.MapProducts<ProductQueryModel>(p).Result;
            mappedProducts.Add(mappedProduct);
        });

        #endregion paging

        var returnData = search.SetData(mappedProducts).SetPaging(pager);

        #region Price Order

        switch (search.SearchProductPriceOrder)
        {
            case SearchProductPriceOrder.Price_Asc:
                returnData.Products = returnData.Products.OrderBy(x => x.UnitPrice);
                break;

            case SearchProductPriceOrder.Price_Des:
                returnData.Products = returnData.Products.OrderByDescending(x => x.UnitPrice);
                break;
        }

        #endregion

        if (returnData.Products is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<SearchProductQueryModel>(returnData);
    }

    #endregion

    #region Get Product Details

    public async Task<Response<ProductDetailsQueryModel>> GetProductDetails(string slug)
    {
        if (string.IsNullOrEmpty(slug))
            throw new NotFoundApiException();

        var products = await _shopContext.Products.Select(x => new
        {
            x.Slug,
            x.Id
        }).ToListAsync();

        bool existsProduct = false;
        long existsProductId = 0;

        var existsProductInto = products.FirstOrDefault(x => x.Slug == slug);

        if (existsProductInto is not null)
        {
            existsProductId = existsProductInto.Id;
            existsProduct = true;
        }

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این مشخصات پیدا نشد");

        var product = _shopContext.Products
                .Include(p => p.ProductPictures)
                .Include(p => p.ProductFeatures)
                .Where(p => p.Id == existsProductId)
                .AsQueryable()
                .Select(p =>
                   _mapper.Map(p, new ProductDetailsQueryModel()))
                .FirstOrDefault();

        product = await _productHelper.MapProducts<ProductDetailsQueryModel>(product);

        var inventory = await _productHelper.GetProductInventory(product.Id);

        product.InventoryCurrentCount = inventory.Item3;
        product.ProductPictures = _productHelper.GetProductPictures(product.Id);
        product.ProductFeatures = _productHelper.GetProductFeatures(product.Id);

        return new Response<ProductDetailsQueryModel>(product);
    }

    #endregion

    #region GetProductPriceById

    private double GetProductPriceById(long id)
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
