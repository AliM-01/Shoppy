using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Queries;

public record FilterOrdersQuery
    (FilterOrderDto Filter) : IRequest<ApiResult<FilterOrderDto>>;