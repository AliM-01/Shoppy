namespace SM.Application.Contracts.ProductPicture.Commands;

public record RemoveProductPictureCommand
    (long ProductPictureId) : IRequest<Response<string>>;