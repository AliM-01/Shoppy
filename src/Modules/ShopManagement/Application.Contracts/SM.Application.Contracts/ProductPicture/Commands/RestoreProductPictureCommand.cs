namespace SM.Application.Contracts.ProductPicture.Commands;
public class RestoreProductPictureCommand : IRequest<Response<string>>
{
    public RestoreProductPictureCommand(long productPictureId)
    {
        ProductPictureId = productPictureId;
    }

    public long ProductPictureId { get; set; }
}