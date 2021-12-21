using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Commands;

public record CreateProductCategoryCommand
    (CreateProductCategoryDto ProductCategory) : IRequest<Response<string>>;