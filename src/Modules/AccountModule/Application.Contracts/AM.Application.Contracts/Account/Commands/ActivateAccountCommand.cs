using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record ActivateAccountCommand
    (ActivateAccountRequestDto Account) : IRequest<ApiResult>;
