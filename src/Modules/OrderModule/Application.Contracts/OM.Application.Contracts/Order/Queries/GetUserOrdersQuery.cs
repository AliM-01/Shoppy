using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Queries;

public record GetUserOrdersQuery(string UserId) : IRequest<ApiResult<List<OrderDto>>>;
