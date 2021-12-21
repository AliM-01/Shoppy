namespace SM.Application.Contracts.Product.Commands;

public record DeleteProductCommand(long ProductId) : IRequest<Response<string>>;