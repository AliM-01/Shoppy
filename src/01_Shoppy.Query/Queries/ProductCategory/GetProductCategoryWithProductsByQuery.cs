using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure;
using _01_Shoppy.Query.Helpers.Product;
using _01_Shoppy.Query.Models.ProductCategory;

namespace _01_Shoppy.Query.Queries.ProductCategory;

public record GetProductCategoryWithProductsByQuery(FilterProductCategoryDetailsModel Filter) : IRequest<ProductCategoryDetailsQueryModel>;

public class GetProductCategoryWithProductsByQueryHandler : IRequestHandler<GetProductCategoryWithProductsByQuery, ProductCategoryDetailsQueryModel>
{
    #region Ctor

    private readonly IRepository<SM.Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductCategoryWithProductsByQueryHandler(IRepository<SM.Domain.ProductCategory.ProductCategory> productCategoryRepository,
                                                        IProductHelper productHelper,
                                                        IMapper mapper,
                                                        IRepository<SM.Domain.Product.Product> productRepository)
    {
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ProductCategoryDetailsQueryModel> Handle(GetProductCategoryWithProductsByQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Filter.Slug))
            throw new NotFoundApiException();

        var categories = await _productCategoryRepository.AsQueryable()
            .Select(x => new
            {
                x.Slug,
                x.Id
            }).ToListAsyncSafe();

        string categoryId = "";

        #region filter

        var category = categories.FirstOrDefault(x => x.Slug == request.Filter.Slug);

        NotFoundApiException.ThrowIfNull(category);

        categoryId = category.Id;

        #endregion

        #region paging

        var productCategoryData = await _productCategoryRepository.FindByIdAsync(categoryId);

        var productsQuery = _productRepository.AsQueryable()
             .Where(x => x.CategoryId == productCategoryData.Id);

        var pager = request.Filter.BuildPager((await productsQuery.CountAsync()), cancellationToken);

        var allEntities =
             _productRepository
             .ApplyPagination(productsQuery, pager)
             .Select(p =>
               _productHelper.MapProductsFromProductCategories(p).Result)
             .ToList();

        #endregion

        var filteredData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (filteredData.Products is null)
            throw new NoContentApiException();

        if (filteredData.PageId > filteredData.GetLastPage() && filteredData.GetLastPage() != 0)
            throw new NotFoundApiException();

        var returnData = new ProductCategoryDetailsQueryModel();

        returnData.ProductCategory = _mapper.Map(productCategoryData, new ProductCategoryQueryModel());
        returnData.FilterData = filteredData;

        return returnData;
    }
}
