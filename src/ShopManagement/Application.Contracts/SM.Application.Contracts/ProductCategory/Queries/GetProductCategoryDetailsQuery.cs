using _0_Framework.Application.Wrappers;
using MediatR;
using SM.Application.Contracts.ProductCategory.DTOs;

namespace SM.Application.Contracts.ProductCategory.Queries
{
    public class GetProductCategoryDetailsQuery : IRequest<Response<EditProductCategoryDto>>
    {
        public GetProductCategoryDetailsQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}