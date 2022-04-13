using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record AuthenticateUserCommand
    (AuthenticateUserRequestDto Account) : IRequest<ApiResult<AuthenticateUserResponseDto>>;
