using _0_Framework.Application.ErrorMessages;
using _0_Framework.Application.Exceptions;
using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Helpers.Product;
using _01_Shoppy.Query.Models.ProductCategory;
using AutoMapper;

namespace _01_Shoppy.Query.Queries.ProductCategory;

public record GetProductCategoryWithProductsByQuery(FilterProductCategoryDetailsModel Filter) : IRequest<Response<ProductCategoryDetailsQueryModel>>;

public class GetProductCategoryWithProductsByQueryHandler : IRequestHandler<GetProductCategoryWithProductsByQuery, Response<ProductCategoryDetailsQueryModel>>
{
    #region Ctor

    private readonly IGenericRepository<SM.Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IGenericRepository<SM.Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductCategoryWithProductsByQueryHandler(IGenericRepository<SM.Domain.ProductCategory.ProductCategory> productCategoryRepository,
                                                        IProductHelper productHelper,
                                                        IMapper mapper,
                                                        IGenericRepository<SM.Domain.Product.Product> productRepository)
    {
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ProductCategoryDetailsQueryModel>> Handle(GetProductCategoryWithProductsByQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Filter.Slug))
            throw new NotFoundApiException();

        var categories = (await _productCategoryRepository.AsQueryable().ToListAsyncSafe())
            .Select(x => new
            {
                x.Slug,
                x.Id
            }).ToList();

        string categoryId = "";

        #region filter

        var category = categories.FirstOrDefault(x => x.Slug == request.Filter.Slug);

        if (category is null)
            throw new NotFoundApiException();

        categoryId = category.Id;

        #endregion

        #region paging

        var productCategoryData = await _productCategoryRepository.GetByIdAsync(categoryId);

        var productsQuery = _productRepository.AsQueryable()
             .Where(x => x.CategoryId == productCategoryData.Id);

        var pager = request.Filter.BuildPager(productsQuery.Count());

        var allEntities =
             _productRepository
             .ApplyPagination(productsQuery, pager)
             .Select(p =>
               _productHelper.MapProductsFromProductCategories(p).Result)
             .ToList();

        #endregion

        var filteredData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (filteredData.Products is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (filteredData.PageId > filteredData.GetLastPage() && filteredData.GetLastPage() != 0)
            throw new NotFoundApiException();

        var returnData = new ProductCategoryDetailsQueryModel();

        returnData.ProductCategory = _mapper.Map(productCategoryData, new ProductCategoryQueryModel());
        returnData.FilterData = filteredData;

        return new Response<ProductCategoryDetailsQueryModel>(returnData);
    }
}
