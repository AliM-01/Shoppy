using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Commands;

public record EditProductCommand
    (EditProductDto Product) : IRequest<Response<string>>;