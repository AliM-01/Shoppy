using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;
public class ExistsProductIdQuery : IRequest<Response<ExistsProductIdResponseDto>>
{
    public ExistsProductIdQuery(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; set; }
}