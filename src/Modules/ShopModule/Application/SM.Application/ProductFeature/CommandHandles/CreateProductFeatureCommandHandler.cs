using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Application.ProductFeature.CommandHandles;

public class CreateProductFeatureCommandHandler : IRequestHandler<CreateProductFeatureCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public CreateProductFeatureCommandHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(CreateProductFeatureCommand request, CancellationToken cancellationToken)
    {
        if (await _productFeatureRepository.ExistsAsync(x => x.ProductId == request.ProductFeature.ProductId
                && x.FeatureTitle == request.ProductFeature.FeatureTitle))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var productFeature =
            _mapper.Map(request.ProductFeature, new Domain.ProductFeature.ProductFeature());

        await _productFeatureRepository.InsertAsync(productFeature);

        return ApiResponse.Success();
    }
}