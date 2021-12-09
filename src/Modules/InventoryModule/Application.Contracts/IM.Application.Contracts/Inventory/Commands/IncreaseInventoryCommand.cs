using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public class IncreaseInventoryCommand : IRequest<Response<string>>
{
    public IncreaseInventoryCommand(IncreaseInventoryDto inventory)
    {
        Inventory = inventory;
    }

    public IncreaseInventoryDto Inventory { get; set; }
}