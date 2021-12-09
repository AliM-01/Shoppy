using IM.Application.Contracts.Inventory.DTOs;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.Commands;

public class DecreaseInventoryCommand : IRequest<Response<string>>
{
    public DecreaseInventoryCommand(List<DecreaseInventoryDto> inventories)
    {
        Inventories = inventories;
    }

    public List<DecreaseInventoryDto> Inventories { get; set; }
}