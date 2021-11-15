using SM.Application.Contracts.ProductPicture.Commands;

namespace SM.Application.ProductPicture.CommandHandles;

public class RestoreProductPictureCommandHandler : IRequestHandler<RestoreProductPictureCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;

    public RestoreProductPictureCommandHandler(IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RestoreProductPictureCommand request, CancellationToken cancellationToken)
    {
        var productPicture = await _productPictureRepository.GetEntityById(request.ProductPictureId);

        if (productPicture is null)
            throw new NotFoundApiException();

        productPicture.IsDeleted = false;

        _productPictureRepository.Update(productPicture);
        await _productPictureRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}