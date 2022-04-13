using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Queries;

public record GetInventoryOperationLogQuery
    (string Id) : IRequest<ApiResult<InventoryLogsDto>>;