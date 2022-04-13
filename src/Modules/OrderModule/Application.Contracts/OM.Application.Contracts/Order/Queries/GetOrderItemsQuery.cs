using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Queries;

public record GetOrderItemsQuery
    (string OrderId, string UserId, bool IsAdmin) : IRequest<ApiResult<List<OrderItemDto>>>;