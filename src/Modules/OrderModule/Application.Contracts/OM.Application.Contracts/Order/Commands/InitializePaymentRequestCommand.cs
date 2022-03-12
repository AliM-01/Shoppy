using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Commands;

public record InitializePaymentRequestCommand(string OrderId, string Amount, string CallBackUrl) : IRequest<Response<InitializePaymentResponseDto>>;
