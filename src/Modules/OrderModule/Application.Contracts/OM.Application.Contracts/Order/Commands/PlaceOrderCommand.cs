using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Commands;

public record PlaceOrderCommand(CartDto Cart) : IRequest<Response<long>>;
