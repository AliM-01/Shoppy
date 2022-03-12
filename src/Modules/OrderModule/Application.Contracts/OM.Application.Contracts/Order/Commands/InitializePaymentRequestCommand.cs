using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Commands;

public record InitializePaymentRequestCommand(string OrderId) : IRequest<Response<InitializePaymentResponseDto>>;
