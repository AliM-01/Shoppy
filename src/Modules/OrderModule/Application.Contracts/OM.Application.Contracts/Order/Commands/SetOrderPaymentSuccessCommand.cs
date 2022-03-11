namespace OM.Application.Contracts.Order.Commands;

public record SetOrderPaymentSuccessCommand(string OrderId) : IRequest<Response<string>>;
