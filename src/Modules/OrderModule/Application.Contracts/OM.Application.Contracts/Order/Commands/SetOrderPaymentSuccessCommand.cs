namespace OM.Application.Contracts.Order.Commands;

public record SetOrderPaymentSuccessCommand(string OrderId, long RefId) : IRequest<Response<string>>;
