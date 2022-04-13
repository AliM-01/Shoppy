using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Commands;

public record EditProductCategoryCommand
    (EditProductCategoryDto ProductCategory) : IRequest<ApiResult>;