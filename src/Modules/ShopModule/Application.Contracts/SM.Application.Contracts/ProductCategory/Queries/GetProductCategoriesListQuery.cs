using SM.Application.ProductCategory.DTOs;
using System.Collections.Generic;

namespace SM.Application.ProductCategory.Queries;

public record GetProductCategoriesListQuery : IRequest<IEnumerable<ProductCategoryForSelectListDto>>;