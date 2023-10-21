using SM.Application.ProductCategory.DTOs;

namespace SM.Application.ProductCategory.Commands;

public record EditProductCategoryCommand
    (EditProductCategoryDto ProductCategory) : IRequest<ApiResult>;