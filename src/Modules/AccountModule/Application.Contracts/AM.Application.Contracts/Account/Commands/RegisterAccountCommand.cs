using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record RegisterAccountCommand
    (RegisterAccountRequestDto Account) : IRequest<ApiResult<string>>;
