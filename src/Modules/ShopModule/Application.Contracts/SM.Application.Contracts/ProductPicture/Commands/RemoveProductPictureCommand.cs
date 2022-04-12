namespace SM.Application.Contracts.ProductPicture.Commands;

public record RemoveProductPictureCommand
    (string ProductPictureId) : IRequest<ApiResult>;