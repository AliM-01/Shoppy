namespace OM.Application.Contracts.Order.Commands;

public record CancelByAdminOrderCommand(string OrderId) : IRequest<Response<string>>;
