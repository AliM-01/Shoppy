using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public record CreateInventoryCommand(CreateInventoryDto Inventory) : IRequest<ApiResult>;