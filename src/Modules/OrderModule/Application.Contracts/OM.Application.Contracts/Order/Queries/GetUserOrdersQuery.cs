using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Queries;

public record GetUserOrdersQuery(string UserId, GetUserOrdersDto Filter) : IRequest<ApiResult<GetUserOrdersDto>>;
