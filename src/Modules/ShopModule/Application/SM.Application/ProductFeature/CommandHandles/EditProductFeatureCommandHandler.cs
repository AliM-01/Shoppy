using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Application.ProductFeature.CommandHandles;

public class EditProductFeatureCommandHandler : IRequestHandler<EditProductFeatureCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public EditProductFeatureCommandHandler(IGenericRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<string>> Handle(EditProductFeatureCommand request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.GetEntityById(request.ProductFeature.Id);

        if (productFeature is null)
            throw new NotFoundApiException();

        if (_productFeatureRepository.Exists(x => x.FeatureTitle == request.ProductFeature.FeatureTitle && x.Id != request.ProductFeature.Id))
            throw new ApiException(ApplicationErrorMessage.IsDuplicatedMessage);

        _mapper.Map(request.ProductFeature, productFeature);

        _productFeatureRepository.Update(productFeature);
        await _productFeatureRepository.SaveChanges();

        return new Response<string>();
    }
}