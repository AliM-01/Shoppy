using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Helpers.Product;
using AutoMapper;
using IM.Domain.Inventory;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.Product;

public record SearchQuery(SearchProductQueryModel Search) : IRequest<Response<SearchProductQueryModel>>;

public class SearchQueryHandler : IRequestHandler<SearchQuery, Response<SearchProductQueryModel>>
{
    #region Ctor

    private readonly ShopDbContext _shopContext;
    private readonly IProductHelper _productHelper;
    private readonly IMongoHelper<Inventory> _inventoryContext;
    private readonly IMapper _mapper;

    public SearchQueryHandler(
        ShopDbContext shopContext,
         IMongoHelper<Inventory> inventoryContext, IProductHelper productHelper, IMapper mapper)
    {
        _shopContext = Guard.Against.Null(shopContext, nameof(_shopContext));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _inventoryContext = Guard.Against.Null(inventoryContext, nameof(_inventoryContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<SearchProductQueryModel>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var query = _shopContext.Products
               .OrderByDescending(x => x.LastUpdateDate)
               .AsQueryable();

        #region inventories query

        var inventories = (await _inventoryContext.AsQueryable().ToListAsyncSafe())
            .Select(x => new
            {
                x.ProductId,
                x.InStock,
                x.UnitPrice
            }).ToList();

        var inventoryIds = inventories.Select(x => x.ProductId).ToArray();

        query = query.Where(p => inventoryIds.Contains(p.Id));

        #endregion

        #region filter selected categories slugs

        if (request.Search.SelectedCategories is not null && request.Search.SelectedCategories.Any())
        {
            var selectedCategoriesId = new List<long>();

            foreach (var categorySlug in request.Search.SelectedCategories)
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

        var maxPrice = inventories?.OrderByDescending(p => p.UnitPrice).FirstOrDefault();
        request.Search.FilterMaxPrice = maxPrice != null ? maxPrice.UnitPrice : 0;

        if (request.Search.SelectedMaxPrice == 0)
            request.Search.SelectedMaxPrice = maxPrice != null ? maxPrice.UnitPrice : 0;

        query = query.ToArray()
            .Where(p => _productHelper.GetProductPriceById(p.Id) >= request.Search.SelectedMinPrice)
            .AsQueryable();

        query = query.ToArray()
            .Where(p => _productHelper.GetProductPriceById(p.Id) <= request.Search.SelectedMaxPrice)
            .AsQueryable();

        #endregion

        #region filter phrase

        if (!string.IsNullOrEmpty(request.Search.Phrase))
        {
            query = query.Where(s => s.Title.Contains(request.Search.Phrase)
            || s.ShortDescription.Contains(request.Search.Phrase));
        }

        #endregion

        #region filter sort date order

        switch (request.Search.SortDateOrder)
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

        var pager = Pager.Build(request.Search.PageId, query.ToList().Count,
            request.Search.TakePage, request.Search.ShownPages);
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

        var returnData = request.Search.SetData(mappedProducts).SetPaging(pager);

        #region Price Order

        switch (request.Search.SearchProductPriceOrder)
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
}
