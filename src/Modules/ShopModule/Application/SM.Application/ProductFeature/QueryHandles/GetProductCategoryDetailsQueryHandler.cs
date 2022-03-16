using SM.Application.Contracts.ProductFeature.DTOs;
using SM.Application.Contracts.ProductFeature.Queries;

namespace SM.Application.ProductFeature.QueryHandles;
public class GetProductFeatureDetailsQueryHandler : IRequestHandler<GetProductFeatureDetailsQuery, Response<EditProductFeatureDto>>
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

    public async Task<Response<EditProductFeatureDto>> Handle(GetProductFeatureDetailsQuery request, CancellationToken cancellationToken)
    {
        var ProductFeature = await _productFeatureRepository.GetByIdAsync(request.Id);

        if (ProductFeature is null)
            throw new NotFoundApiException();

        var mappedProductFeature = _mapper.Map<EditProductFeatureDto>(ProductFeature);

        return new Response<EditProductFeatureDto>(mappedProductFeature);
    }
}