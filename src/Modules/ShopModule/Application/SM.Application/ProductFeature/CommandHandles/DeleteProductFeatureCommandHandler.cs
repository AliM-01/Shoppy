using SM.Application.ProductFeature.Commands;

namespace SM.Application.ProductFeature.CommandHandles;

public class DeleteProductFeatureCommandHandler : IRequestHandler<DeleteProductFeatureCommand, ApiResult>
{
    #region Ctor

    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;

    public DeleteProductFeatureCommandHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
    }

    #endregion

    public async Task<ApiResult> Handle(DeleteProductFeatureCommand request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.FindByIdAsync(request.ProductFeatureId);

        NotFoundApiException.ThrowIfNull(productFeature);

        await _productFeatureRepository.DeletePermanentAsync(productFeature.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}