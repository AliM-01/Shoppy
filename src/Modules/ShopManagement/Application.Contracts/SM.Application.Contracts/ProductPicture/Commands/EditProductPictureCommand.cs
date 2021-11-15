using SM.Application.Contracts.ProductPicture.DTOs;

namespace SM.Application.Contracts.ProductPicture.Commands;

public class EditProductPictureCommand : IRequest<Response<string>>
{
    public EditProductPictureCommand(EditProductPictureDto productPicture)
    {
        ProductPicture = productPicture;
    }

    public EditProductPictureDto ProductPicture { get; set; }
}