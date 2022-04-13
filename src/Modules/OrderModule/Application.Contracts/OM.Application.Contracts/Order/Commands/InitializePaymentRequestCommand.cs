using OM.Application.Contracts.Order.DTOs;

namespace OM.Application.Contracts.Order.Commands;

public record InitializePaymentRequestCommand(InitializePaymentRequestDto Payment) : IRequest<ApiResult<InitializePaymentResponseDto>>;
