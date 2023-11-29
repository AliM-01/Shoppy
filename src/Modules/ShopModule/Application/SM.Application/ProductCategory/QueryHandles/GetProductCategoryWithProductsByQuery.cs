using _0_Framework.Application.Models.Paging;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SM.Application.ProductCategory.DTOs;
using SM.Application.Services;
using MongoDB.Driver.Linq;

namespace SM.Application.ProductCategory.QueryHandles;

public record GetProductCategoryWithProductsByQuery(FilterProductCategoryDetailsDto Filter) : IRequest<ProductCategoryDetailsDto>;

public class GetProductCategoryWithProductsByQueryHandler : IRequestHandler<GetProductCategoryWithProductsByQuery, ProductCategoryDetailsDto>
{
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IRepository<Domain.Product.Product> _productRepository;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductCategoryWithProductsByQueryHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository,
                                                        IProductHelper productHelper,
                                                        IMapper mapper,
                                                        IRepository<Domain.Product.Product> productRepository)
    {
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ProductCategoryDetailsDto> Handle(GetProductCategoryWithProductsByQuery request, CancellationToken cancellationToken)
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

        var pager = request.Filter.BuildPager(await productsQuery.CountAsync(), cancellationToken);

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

        var returnData = new ProductCategoryDetailsDto();

        returnData.ProductCategory = _mapper.Map(productCategoryData, new SiteProductCategoryDto());
        returnData.FilterData = filteredData;

        return returnData;
    }
}
