using SM.Application.Contracts.ProductFeature.Commands;

namespace SM.Application.ProductFeature.CommandHandles;

public class DeleteProductFeatureCommandHandler : IRequestHandler<DeleteProductFeatureCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;

    public DeleteProductFeatureCommandHandler(IGenericRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(DeleteProductFeatureCommand request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.GetEntityById(request.ProductFeatureId);

        if (productFeature is null)
            throw new NotFoundApiException();

        await _productFeatureRepository.FullDelete(productFeature.Id);
        await _productFeatureRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}