using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Helpers.Product;
using IM.Domain.Inventory;

namespace _01_Shoppy.Query.Queries.Product;

public record SearchQuery(SearchProductQueryModel Search) : IRequest<Response<SearchProductQueryModel>>;

public class SearchQueryHandler : IRequestHandler<SearchQuery, Response<SearchProductQueryModel>>
{
    #region Ctor

    private readonly IGenericRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IGenericRepository<SM.Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IProductHelper _productHelper;
    private readonly IGenericRepository<Inventory> _inventoryContext;
    private readonly IMapper _mapper;

    public SearchQueryHandler(IGenericRepository<SM.Domain.Product.Product> productRepository,
                              IGenericRepository<SM.Domain.ProductCategory.ProductCategory> productCategoryRepository,
                              IGenericRepository<Inventory> inventoryContext,
                              IProductHelper productHelper,
                              IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _inventoryContext = Guard.Against.Null(inventoryContext, nameof(_inventoryContext));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<SearchProductQueryModel>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.AsQueryable();

        #region inventories query

        var inventories = await _inventoryContext.AsQueryable()
            .Select(x => new
            {
                x.ProductId,
                x.InStock,
                x.UnitPrice
            }).ToListAsyncSafe();

        var inventoryIds = inventories.Select(x => x.ProductId).ToArray();

        query = query.Where(p => inventoryIds.Contains(p.Id));

        #endregion

        #region filter selected categories slugs

        if (request.Search.SelectedCategories is not null && request.Search.SelectedCategories.Any())
        {
            var selectedCategoriesId = new List<string>();

            foreach (var categorySlug in request.Search.SelectedCategories)
            {
                if (await _productCategoryRepository.ExistsAsync(x => x.Slug == categorySlug))
                {
                    var filter = Builders<SM.Domain.ProductCategory.ProductCategory>.Filter.Eq(x => x.Slug, categorySlug);
                    var category = await _productCategoryRepository.GetByFilter(filter);
                    selectedCategoriesId.Add(category.Id);
                }
            }

            query = query.Where(x => selectedCategoriesId.Contains(x.CategoryId));
        }

        #endregion

        #region filter

        #region filter price

        var maxPrice = inventories?.OrderByDescending(p => p.UnitPrice).FirstOrDefault();
        request.Search.FilterMaxPrice = maxPrice != null ? maxPrice.UnitPrice : 0;

        if (request.Search.SelectedMaxPrice == 0)
            request.Search.SelectedMaxPrice = maxPrice != null ? maxPrice.UnitPrice : 0;

        #endregion

        #region filter phrase

        if (!string.IsNullOrEmpty(request.Search.Phrase))
        {
            query = query.Where(s => s.Title.Contains(request.Search.Phrase)
            || s.ShortDescription.Contains(request.Search.Phrase) || s.MetaKeywords.Contains(request.Search.Phrase));
        }

        #endregion

        #region filter sort date order

        switch (request.Search.SortDateOrder)
        {
            case PagingDataSortCreationDateOrder.DES:
                query = query.OrderByDescending(x => x.CreationDate);
                break;

            case PagingDataSortCreationDateOrder.ASC:
                query = query.OrderBy(x => x.CreationDate);
                break;
        }

        #endregion

        #endregion filter

        #region paging

        var pager = request.Search.BuildPager((await query.CountAsync()));

        var allEntities =
             _productRepository
             .ApplyPagination(query, pager)
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


        returnData.Products = returnData.Products.Where(p => _productHelper.GetProductPriceById(p.Id) >= request.Search.SelectedMinPrice);

        returnData.Products = returnData.Products.Where(p => _productHelper.GetProductPriceById(p.Id) <= request.Search.SelectedMaxPrice);


        #endregion

        if (returnData.Products is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<SearchProductQueryModel>(returnData);
    }
}
