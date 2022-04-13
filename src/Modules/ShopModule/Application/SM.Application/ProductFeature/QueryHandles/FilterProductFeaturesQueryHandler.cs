using _0_Framework.Application.Models.Paging;
using MongoDB.Driver.Linq;
using SM.Application.Contracts.ProductFeature.DTOs;
using SM.Application.Contracts.ProductFeature.Queries;
using System.Linq;

namespace SM.Application.ProductFeature.QueryHandles;
public class FilterProductFeaturesQueryHandler : IRequestHandler<FilterProductFeaturesQuery, ApiResult<FilterProductFeatureDto>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public FilterProductFeaturesQueryHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult<FilterProductFeatureDto>> Handle(FilterProductFeaturesQuery request, CancellationToken cancellationToken)
    {
        var query = _productFeatureRepository.AsQueryable();

        if (string.IsNullOrEmpty(request.Filter.ProductId))
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFound);

        #region filter

        query = query.Where(s => s.ProductId == request.Filter.ProductId);

        #endregion filter

        #region paging

        var pager = request.Filter.BuildPager((await query.CountAsync()));

        var allEntities =
             _productFeatureRepository
             .ApplyPagination(query, pager)
             .Select(product =>
                _mapper.Map(product, new ProductFeatureDto()))
             .ToList();

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.ProductFeatures is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFound);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return ApiResponse.Success<FilterProductFeatureDto>(returnData);
    }
}