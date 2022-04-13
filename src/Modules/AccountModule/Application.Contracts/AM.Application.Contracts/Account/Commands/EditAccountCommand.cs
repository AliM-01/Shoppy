using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record EditAccountCommand
    (EditAccountDto Account) : IRequest<ApiResult>;
