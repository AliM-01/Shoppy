namespace OM.Application.Contracts.Order.Commands;

public record CancelOrderByAdminCommand(string OrderId) : IRequest<Response<string>>;
