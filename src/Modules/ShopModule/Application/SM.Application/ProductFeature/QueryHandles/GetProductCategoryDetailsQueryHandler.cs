using SM.Application.Contracts.ProductFeature.DTOs;
using SM.Application.Contracts.ProductFeature.Queries;

namespace SM.Application.ProductFeature.QueryHandles;
public class GetProductFeatureDetailsQueryHandler : IRequestHandler<GetProductFeatureDetailsQuery, EditProductFeatureDto>
{
    #region Ctor

    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public GetProductFeatureDetailsQueryHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<EditProductFeatureDto> Handle(GetProductFeatureDetailsQuery request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(productFeature);

        return _mapper.Map<EditProductFeatureDto>(productFeature);
    }
}