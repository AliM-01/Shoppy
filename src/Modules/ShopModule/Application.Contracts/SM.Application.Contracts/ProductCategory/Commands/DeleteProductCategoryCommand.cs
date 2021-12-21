namespace SM.Application.Contracts.ProductCategory.Commands;

public record DeleteProductCategoryCommand
    (long ProductCategoryId) : IRequest<Response<string>>;