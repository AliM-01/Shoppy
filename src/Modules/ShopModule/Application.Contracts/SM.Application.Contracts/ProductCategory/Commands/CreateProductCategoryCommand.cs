using SM.Application.ProductCategory.DTOs;

namespace SM.Application.ProductCategory.Commands;

public record CreateProductCategoryCommand
    (CreateProductCategoryDto ProductCategory) : IRequest<ApiResult>;