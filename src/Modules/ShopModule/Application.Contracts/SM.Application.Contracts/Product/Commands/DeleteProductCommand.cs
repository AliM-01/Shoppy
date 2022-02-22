namespace SM.Application.Contracts.Product.Commands;

public record DeleteProductCommand(string ProductId) : IRequest<Response<string>>;