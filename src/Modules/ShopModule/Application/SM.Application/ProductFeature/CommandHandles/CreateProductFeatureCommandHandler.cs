using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Application.ProductFeature.CommandHandles;

public class CreateProductFeatureCommandHandler : IRequestHandler<CreateProductFeatureCommand, Response<string>>
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

    public async Task<Response<string>> Handle(CreateProductFeatureCommand request, CancellationToken cancellationToken)
    {
        if (await _productFeatureRepository.ExistsAsync(x => x.ProductId == request.ProductFeature.ProductId
                && x.FeatureTitle == request.ProductFeature.FeatureTitle))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        var productFeature =
            _mapper.Map(request.ProductFeature, new Domain.ProductFeature.ProductFeature());

        await _productFeatureRepository.InsertAsync(productFeature);

        return new Response<string>(ApplicationErrorMessage.OperationSucceddedMessage);
    }
}