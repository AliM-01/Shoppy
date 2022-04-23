using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Application.ProductFeature.CommandHandles;

public class EditProductFeatureCommandHandler : IRequestHandler<EditProductFeatureCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public EditProductFeatureCommandHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<ApiResult> Handle(EditProductFeatureCommand request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.FindByIdAsync(request.ProductFeature.Id);

        if (productFeature is null)
            throw new NotFoundApiException();

        if (await _productFeatureRepository.ExistsAsync(x => x.ProductId == request.ProductFeature.ProductId
                && x.FeatureTitle == request.ProductFeature.FeatureTitle && x.Id != request.ProductFeature.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.ProductFeature, productFeature);

        await _productFeatureRepository.UpdateAsync(productFeature);

        return ApiResponse.Success();
    }
}