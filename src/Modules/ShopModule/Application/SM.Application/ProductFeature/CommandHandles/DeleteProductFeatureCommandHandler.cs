using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Application.ProductFeature.CommandHandles;

public class DeleteProductFeatureCommandHandler : IRequestHandler<DeleteProductFeatureCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;

    public DeleteProductFeatureCommandHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteProductFeatureCommand request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.GetByIdAsync(request.ProductFeatureId);

        if (productFeature is null)
            throw new NotFoundApiException();

        await _productFeatureRepository.DeletePermanentAsync(productFeature.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeleted);
    }
}