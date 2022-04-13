using SM.Application.Contracts.ProductPicture.DTOs;

namespace SM.Application.Contracts.ProductPicture.Commands;

public record CreateProductPictureCommand
    (CreateProductPictureDto ProductPicture) : IRequest<ApiResult>;