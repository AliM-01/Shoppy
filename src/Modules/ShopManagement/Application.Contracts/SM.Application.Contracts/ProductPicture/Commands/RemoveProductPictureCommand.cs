namespace SM.Application.Contracts.ProductPicture.Commands;
public class RemoveProductPictureCommand : IRequest<Response<string>>
{
    public RemoveProductPictureCommand(long productPictureId)
    {
        ProductPictureId = productPictureId;
    }

    public long ProductPictureId { get; set; }
}