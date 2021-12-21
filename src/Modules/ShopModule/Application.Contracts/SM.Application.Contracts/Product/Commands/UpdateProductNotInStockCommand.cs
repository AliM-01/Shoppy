namespace SM.Application.Contracts.Product.Queries;

public record UpdateProductNotInStockCommand
    (long ProductId) : IRequest<Response<string>>;