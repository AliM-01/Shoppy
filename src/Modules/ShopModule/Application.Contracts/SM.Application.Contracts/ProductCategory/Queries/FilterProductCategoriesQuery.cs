using SM.Application.ProductCategory.DTOs;

namespace SM.Application.ProductCategory.Queries;

public record FilterProductCategoriesQuery(FilterProductCategoryDto Filter) : IRequest<FilterProductCategoryDto>;