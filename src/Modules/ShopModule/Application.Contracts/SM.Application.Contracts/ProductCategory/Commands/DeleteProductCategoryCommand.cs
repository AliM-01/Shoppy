namespace SM.Application.Contracts.ProductCategory.Commands;

public record DeleteProductCategoryCommand
    (string ProductCategoryId) : IRequest<ApiResult>;