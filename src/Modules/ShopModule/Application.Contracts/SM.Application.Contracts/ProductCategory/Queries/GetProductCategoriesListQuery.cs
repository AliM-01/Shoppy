using SM.Application.Contracts.ProductCategory.DTOs;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductCategory.Queries;

public record GetProductCategoriesListQuery
    : IRequest<Response<IEnumerable<ProductCategoryForSelectListDto>>>;