using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Queries;

public record CheckoutCartQuery
    (List<CartItemInCookieDto> Items, string? DiscountCodeId) : IRequest<ApiResult<CartDto>>;