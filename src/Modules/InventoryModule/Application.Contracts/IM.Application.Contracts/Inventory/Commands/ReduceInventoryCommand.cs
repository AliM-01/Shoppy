using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public class ReduceInventoryCommand : IRequest<Response<string>>
{
    public ReduceInventoryCommand(ReduceInventoryDto inventory)
    {
        Inventory = inventory;
    }

    public ReduceInventoryDto Inventory { get; set; }
}