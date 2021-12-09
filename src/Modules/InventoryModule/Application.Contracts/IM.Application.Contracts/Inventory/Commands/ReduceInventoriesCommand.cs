using IM.Application.Contracts.Inventory.DTOs;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoriesCommand : IRequest<Response<string>>
{
    public ReduceInventoriesCommand(List<ReduceInventoryDto> inventories)
    {
        Inventories = inventories;
    }

    public List<ReduceInventoryDto> Inventories { get; set; }
}