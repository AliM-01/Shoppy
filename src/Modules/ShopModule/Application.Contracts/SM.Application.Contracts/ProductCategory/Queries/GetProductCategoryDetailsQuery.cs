using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Queries;

public record GetProductCategoryDetailsQuery
    (string Id) : IRequest<Response<EditProductCategoryDto>>;