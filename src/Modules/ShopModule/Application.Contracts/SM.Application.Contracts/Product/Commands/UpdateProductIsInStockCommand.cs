namespace SM.Application.Contracts.Product.Queries;

public record UpdateProductIsInStockCommand
    (long ProductId) : IRequest<Response<string>>;