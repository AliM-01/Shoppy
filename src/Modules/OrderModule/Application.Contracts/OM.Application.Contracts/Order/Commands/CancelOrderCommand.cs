namespace OM.Application.Contracts.Order.Commands;

public record CancelOrderCommand(string OrderId, string UserId, bool IsAdmin) : IRequest<Response<string>>;
