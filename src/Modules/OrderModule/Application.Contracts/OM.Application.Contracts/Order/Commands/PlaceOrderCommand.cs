using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Commands;

public record PlaceOrderCommand(CartDto Cart, string UserId) : IRequest<Response<long>>;
