using IM.Application.Contracts.Inventory.DTOs;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoryCommand : IRequest<Response<string>>
{
    public ReduceInventoryCommand(List<ReduceInventoryDto> inventories)
    {
        Inventories = inventories;
    }

    public List<ReduceInventoryDto> Inventories { get; set; }
}