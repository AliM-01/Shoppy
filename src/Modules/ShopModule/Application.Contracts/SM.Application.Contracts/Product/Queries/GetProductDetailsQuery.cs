using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;
public class GetProductDetailsQuery : IRequest<Response<EditProductDto>>
{
    public GetProductDetailsQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}