using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Commands;

public record CreateProductCommand
    (CreateProductDto Product) : IRequest<ApiResult<CreateProductResponseDto>>;