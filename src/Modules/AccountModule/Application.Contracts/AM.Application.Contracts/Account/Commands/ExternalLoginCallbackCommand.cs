using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record ExternalLoginCallbackCommand() : IRequest<ApiResult<ExternalLoginCallbackResponseDto>>;