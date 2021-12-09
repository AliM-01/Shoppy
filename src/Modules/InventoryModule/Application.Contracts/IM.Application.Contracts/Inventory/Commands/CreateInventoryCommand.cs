using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public class CreateInventoryCommand : IRequest<Response<string>>
{
    public CreateInventoryCommand(CreateInventoryDto inventory)
    {
        Inventory = inventory;
    }

    public CreateInventoryDto Inventory { get; set; }
}