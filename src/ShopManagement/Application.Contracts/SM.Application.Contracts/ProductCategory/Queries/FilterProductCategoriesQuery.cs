using System.Collections.Generic;
using _0_Framework.Application.Wrappers;
using MediatR;
using SM.Application.Contracts.ProductCategory.Models;

namespace SM.Application.Contracts.ProductCategory.Queries
{
    public class FilterProductCategoriesQuery : IRequest<Response<IEnumerable<ProductCategoryDto>>>
    {
        
    }
}