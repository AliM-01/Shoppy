using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Queries;

public class GetInventoryOperationLogQuery : IRequest<Response<InventoryOperationDto[]>>
{
    public GetInventoryOperationLogQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}