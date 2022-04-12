using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Queries;

public record GetInventoryDetailsQuery(string Id) : IRequest<ApiResult<EditInventoryDto>>;