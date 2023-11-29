using _0_Framework.Application.Models.Paging;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SM.Application.Product.DTOs.Site;
using SM.Application.Services;
using MongoDB.Driver.Linq;

namespace SM.Application.Product.Queries.Site;

public record SearchQuery(SearchProductSiteDto Search) : IRequest<SearchProductSiteDto>;

public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchProductSiteDto>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IProductHelper _productHelper;
    private readonly IInventoryAclService _inventoryAclService;
    private readonly IMapper _mapper;

    public SearchQueryHandler(IRepository<Domain.Product.Product> productRepository,
                              IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository,
                              IInventoryAclService inventoryAclService,
                              IProductHelper productHelper,
                              IMapper mapper)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _inventoryAclService = Guard.Against.Null(inventoryAclService, nameof(_inventoryAclService));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<SearchProductSiteDto> Handle(SearchQuery request, CancellationToken cancellationToken)
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

        decimal maxPrice = _inventoryAclService.GetMaxPrice();

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
                    var filter = Builders<Domain.ProductCategory.ProductCategory>.Filter.Eq(x => x.Slug, categorySlug);
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

        var mappedProducts = new List<ProductSiteDto>();

        for (int i = 0; i < allEntities.Count; i++)
        {
            mappedProducts.Add(await _productHelper.MapProducts<ProductSiteDto>(allEntities[i]));
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

        return request.Search;
    }
}