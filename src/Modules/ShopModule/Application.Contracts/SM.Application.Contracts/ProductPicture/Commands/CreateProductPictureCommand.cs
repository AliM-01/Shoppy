using SM.Application.Contracts.ProductPicture.DTOs;

namespace SM.Application.Contracts.ProductPicture.Commands;

public class CreateProductPictureCommand : IRequest<Response<string>>
{
    public CreateProductPictureCommand(CreateProductPictureDto productPicture)
    {
        ProductPicture = productPicture;
    }

    public CreateProductPictureDto ProductPicture { get; set; }
}