using SM.Application.ProductCategory.DTOs;

namespace SM.Application.ProductCategory.Queries;

public record GetProductCategoryDetailsQuery(string Id) : IRequest<EditProductCategoryDto>;