using SM.Application.Contracts.ProductCategory.DTOs;
using System.Collections.Generic;

namespace SM.Application.Contracts.ProductCategory.Queries;

public class GetProductCategoriesListQuery : IRequest<Response<IEnumerable<ProductCategoryForSelectListDto>>>
{
    public GetProductCategoriesListQuery()
    {

    }
}