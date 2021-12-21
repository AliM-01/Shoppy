using IM.Application.Contracts.Inventory.DTOs;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.Queries;

public class GetInventoryOperationLogQuery : IRequest<Response<IEnumerable<InventoryOperationDto>>>
{
    public GetInventoryOperationLogQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}