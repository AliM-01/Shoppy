using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public record IncreaseInventoryCommand
    (IncreaseInventoryDto Inventory, string UserId) : IRequest<Response<string>>;