using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Queries;

public class GetInventoryDetailsQuery : IRequest<Response<EditInventoryDto>>
{
    public GetInventoryDetailsQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}