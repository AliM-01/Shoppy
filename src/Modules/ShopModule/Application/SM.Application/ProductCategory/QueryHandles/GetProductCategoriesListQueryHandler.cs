using _0_Framework.Infrastructure;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;
using System.Collections.Generic;
using System.Linq;

namespace SM.Application.ProductCategory.QueryHandles;
public class GetProductCategoriesListQueryHandler : IRequestHandler<GetProductCategoriesListQuery, ApiResult<List<ProductCategoryForSelectListDto>>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public GetProductCategoriesListQueryHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<List<ProductCategoryForSelectListDto>>> Handle(GetProductCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var categories = (
            await _productCategoryRepository
                .AsQueryable()
                .OrderByDescending(p => p.LastUpdateDate)
                .ToListAsyncSafe())
            .Select(product => new ProductCategoryForSelectListDto
            {
                Id = product.Id,
                Title = product.Title
            })
            .ToList();

        if (categories is null)
            throw new NotFoundApiException();

        return ApiResponse.Success<List<ProductCategoryForSelectListDto>>(categories);
    }
}