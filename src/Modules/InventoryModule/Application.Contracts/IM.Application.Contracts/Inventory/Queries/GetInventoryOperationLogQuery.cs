using IM.Application.Contracts.Inventory.DTOs;

namespace IM.Application.Contracts.Inventory.Queries;

public record GetInventoryOperationLogQuery
    (Guid Id) : IRequest<Response<InventoryOperationDto[]>>;