using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Queries;

public record FilterInventoryQuery
    (FilterInventoryDto Filter) : IRequest<Response<FilterInventoryDto>>;