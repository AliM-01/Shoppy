using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public class EditInventoryCommand : IRequest<Response<string>>
{
    public EditInventoryCommand(EditInventoryDto inventory)
    {
        Inventory = inventory;
    }

    public EditInventoryDto Inventory { get; set; }
}