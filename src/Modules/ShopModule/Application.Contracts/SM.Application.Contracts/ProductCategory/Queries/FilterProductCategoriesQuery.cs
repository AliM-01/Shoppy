using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Queries;

public record FilterProductCategoriesQuery
    (FilterProductCategoryDto Filter) : IRequest<Response<FilterProductCategoryDto>>;