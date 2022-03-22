using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Queries;

public record GetInventoryItemsQuery
    (string OrderId, string UserId, bool IsAdmin) : IRequest<Response<List<OrderItemDto>>>;