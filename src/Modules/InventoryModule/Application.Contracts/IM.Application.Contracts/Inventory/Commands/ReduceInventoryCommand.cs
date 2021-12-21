using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Commands;

public record ReduceInventoryCommand
    (ReduceInventoryDto Inventory) : IRequest<Response<string>>;