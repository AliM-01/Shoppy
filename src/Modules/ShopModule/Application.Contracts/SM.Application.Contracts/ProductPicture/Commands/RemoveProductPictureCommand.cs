namespace SM.Application.ProductPicture.Commands;

public record RemoveProductPictureCommand
    (string ProductPictureId) : IRequest<ApiResult>;