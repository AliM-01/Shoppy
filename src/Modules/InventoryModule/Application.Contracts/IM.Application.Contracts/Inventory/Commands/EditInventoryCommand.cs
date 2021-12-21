using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public record EditInventoryCommand(EditInventoryDto Inventory) : IRequest<Response<string>>;