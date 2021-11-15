using SM.Application.Contracts.ProductPicture.Commands;

namespace SM.Application.ProductPicture.CommandHandles;

public class RemoveProductPictureCommandHandler : IRequestHandler<RemoveProductPictureCommand, Response<string>>
{
    #region Ctor

    private readonly IGenericRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;

    public RemoveProductPictureCommandHandler(IGenericRepository<Domain.ProductPicture.ProductPicture> productPictureRepository)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
    }

    #endregion

    public async Task<Response<string>> Handle(RemoveProductPictureCommand request, CancellationToken cancellationToken)
    {
        var productPicture = await _productPictureRepository.GetEntityById(request.ProductPictureId);

        if (productPicture is null)
            throw new NotFoundApiException();

        await _productPictureRepository.SoftDelete(productPicture.Id);
        await _productPictureRepository.SaveChanges();

        return new Response<string>(ApplicationErrorMessage.RecordDeletedMessage);
    }
}