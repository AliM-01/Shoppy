namespace OM.Application.Contracts.Order.Commands;

public record CancelOrderCommand(string OrderId) : IRequest<Response<string>>;
