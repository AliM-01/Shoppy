using SM.Application.Contracts.ProductPicture.Commands;
using System.IO;

namespace SM.Application.ProductPicture.CommandHandles;

public class RemoveProductPictureCommandHandler : IRequestHandler<RemoveProductPictureCommand, Response<string>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;

    public RemoveProductPictureCommandHandler(IRepository<Domain.ProductPicture.ProductPicture> productPictureRepository)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveProductPictureCommand request, CancellationToken cancellationToken)
    {
        var productPicture = await _productPictureRepository.GetByIdAsync(request.ProductPictureId);

        if (productPicture is null)
            throw new NotFoundApiException();

        File.Delete(PathExtension.ProductPictureImage + productPicture.ImagePath);
        File.Delete(PathExtension.ProductPictureThumbnailImage + productPicture.ImagePath);

        await _productPictureRepository.DeletePermanentAsync(productPicture.Id);

        return new Response<string>(ApplicationErrorMessage.RecordDeleted);
    }
}