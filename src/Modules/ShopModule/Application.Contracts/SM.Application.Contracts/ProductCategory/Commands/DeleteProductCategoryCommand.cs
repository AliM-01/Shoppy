namespace SM.Application.ProductCategory.Commands;

public record DeleteProductCategoryCommand
    (string ProductCategoryId) : IRequest<ApiResult>;