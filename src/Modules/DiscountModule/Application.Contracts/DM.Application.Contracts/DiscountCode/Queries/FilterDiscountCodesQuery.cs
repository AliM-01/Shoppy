using DM.Application.Contracts.DiscountCode.DTOs;

namespace DM.Application.Contracts.DiscountCode.Queries;

public record FilterDiscountCodesQuery
    (FilterDiscountCodeDto Filter) : IRequest<ApiResult<FilterDiscountCodeDto>>;