using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Commands;

public record VerifyPaymentRequestCommand(VerifyPaymentRequestDto Payment) : IRequest<Response<string>>;
