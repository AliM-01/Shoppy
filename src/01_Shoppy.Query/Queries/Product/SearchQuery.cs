using _0_Framework.Application.Models.Paging;
using _01_Shoppy.Query.Helpers.Product;
using IM.Domain.Inventory;

namespace _01_Shoppy.Query.Queries.Product;

public record SearchQuery(SearchProductQueryModel Search) : IRequest<ApiResult<SearchProductQueryModel>>;

public class SearchQueryHandler : IRequestHandler<SearchQuery, ApiResult<SearchProductQueryModel>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IRepository<SM.Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IProductHelper _productHelper;
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IMapper _mapper;

    public SearchQueryHandler(IRepository<SM.Domain.Product.Product> productRepository,
                              IRepository<SM.Domain.ProductCategory.ProductCategory> productCategoryRepository,
                              IRepository<Inventory> inventoryRepository,
                              IProductHelper productHelper,
                              IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<SearchProductQueryModel>> Handle(SearchQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = _productRepository.AsQueryable();

        #region filter

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

        #region filter price

        decimal maxPrice = _inventoryRepository.AsQueryable().OrderByDescending(x => x.UnitPrice)
            .Select(x => x.UnitPrice).FirstOrDefault();

        request.Search.FilterMaxPrice = maxPrice;

        if (request.Search.IsPriceMinMaxFilterSelected && request.Search.SelectedMaxPrice == 0)
            request.Search.SelectedMaxPrice = maxPrice;

        #endregion

        #region filter phrase

        if (!string.IsNullOrEmpty(request.Search.Phrase))
        {
            var titleIds = await _productRepository.FullTextSearch(x => x.Title,
                request.Search.Phrase, cancellationToken);

            query = query.Where(x => titleIds.Contains(x.Id));
        }

        #endregion

        #region filter selected categories slugs

        if (request.Search.SelectedCategories is not null && request.Search.SelectedCategories.Any())
        {
            HashSet<string> selectedCategoriesId = default;

            foreach (string categorySlug in request.Search.SelectedCategories)
            {
                if (await _productCategoryRepository.ExistsAsync(x => x.Slug == categorySlug))
                {
                    var filter = Builders<SM.Domain.ProductCategory.ProductCategory>.Filter.Eq(x => x.Slug, categorySlug);
                    var category = await _productCategoryRepository.FindOne(filter);
                    selectedCategoriesId.Add(category.Id);
                }
            }

            query = query.Where(x => selectedCategoriesId.Contains(x.CategoryId));
        }

        #endregion

        #endregion filter

        #region paging

        var pager = request.Search.BuildPager((await query.CountAsync()));

        var allEntities =
             _productRepository
             .ApplyPagination(query, pager);

        var mappedProducts = new List<ProductQueryModel>();

        for (int i = 0; i < allEntities.Count; i++)
        {
            mappedProducts.Add(await _productHelper.MapProducts<ProductQueryModel>(allEntities[i]));
        }

        request.Search.SetData(mappedProducts).SetPaging(pager);

        #endregion paging


        #region Price Order

        switch (request.Search.SearchProductPriceOrder)
        {
            case SearchProductPriceOrder.All:
                break;

            case SearchProductPriceOrder.Price_Asc:
                request.Search.Products = request.Search.Products.OrderBy(x => x.UnitPrice);
                break;

            case SearchProductPriceOrder.Price_Des:
                request.Search.Products = request.Search.Products.OrderByDescending(x => x.UnitPrice);
                break;
        }

        if (request.Search.IsPriceMinMaxFilterSelected)
            request.Search.Products = request.Search.Products.Where(p => p.UnitPrice >= request.Search.SelectedMinPrice && p.UnitPrice <= request.Search.SelectedMaxPrice);

        #endregion

        if (request.Search.Products is null)
            throw new NoContentApiException();

        if (request.Search.PageId > request.Search.GetLastPage() && request.Search.GetLastPage() != 0)
            throw new NotFoundApiException();

        return ApiResponse.Success<SearchProductQueryModel>(request.Search);
    }
}