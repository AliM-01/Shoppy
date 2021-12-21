using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Queries;

public record GetProductCategoryDetailsQuery
    (long Id) : IRequest<Response<EditProductCategoryDto>>;