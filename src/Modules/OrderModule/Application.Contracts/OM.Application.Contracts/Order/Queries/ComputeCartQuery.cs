using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Queries;

public record ComputeCartQuery
    (List<CartItemInCookieDto> Items, bool IsCheckout = false) : IRequest<Response<CartDto>>;