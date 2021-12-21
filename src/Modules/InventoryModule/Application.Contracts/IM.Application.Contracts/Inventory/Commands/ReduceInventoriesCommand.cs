using IM.Application.Contracts.Inventory.DTOs;
using System.Collections.Generic;

namespace IM.Application.Contracts.Inventory.Commands;

public record ReduceInventoriesCommand
    (List<ReduceInventoryDto> Inventories) : IRequest<Response<string>>;