using _0_Framework.Application.Models.Paging;
using Microsoft.EntityFrameworkCore;
using SM.Application.Contracts.ProductFeature.DTOs;
using SM.Application.Contracts.ProductFeature.Queries;
using System.Linq;

namespace SM.Application.ProductFeature.QueryHandles;
public class FilterProductFeaturesQueryHandler : IRequestHandler<FilterProductFeaturesQuery, Response<FilterProductFeatureDto>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public FilterProductFeaturesQueryHandler(IGenericRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<FilterProductFeatureDto>> Handle(FilterProductFeaturesQuery request, CancellationToken cancellationToken)
    {
        var query = _productFeatureRepository.GetQuery().AsQueryable();

        #region filter

        if (request.Filter.ProductId != 0)
            query = query.Where(s => s.ProductId == request.Filter.ProductId);

        #endregion filter

        #region paging

        var pager = Pager.Build(request.Filter.PageId, await query.CountAsync(cancellationToken),
            request.Filter.TakePage, request.Filter.ShownPages);
        var allEntities = await query.Paging(pager)
            .AsQueryable()
            .Select(product =>
                _mapper.Map(product, new ProductFeatureDto()))
            .ToListAsync(cancellationToken);

        #endregion paging

        var returnData = request.Filter.SetData(allEntities).SetPaging(pager);

        if (returnData.ProductFeatures is null)
            throw new ApiException(ApplicationErrorMessage.FilteredRecordsNotFoundMessage);

        if (returnData.PageId > returnData.GetLastPage() && returnData.GetLastPage() != 0)
            throw new NotFoundApiException();

        return new Response<FilterProductFeatureDto>(returnData);
    }
}