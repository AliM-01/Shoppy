using SM.Application.ProductPicture.DTOs;

namespace SM.Application.ProductPicture.Commands;

public record CreateProductPictureCommand
    (CreateProductPictureDto ProductPicture) : IRequest<ApiResult>;