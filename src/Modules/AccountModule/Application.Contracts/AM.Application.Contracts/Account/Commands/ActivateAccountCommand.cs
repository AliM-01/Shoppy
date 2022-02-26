using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record ActivateAccountCommand
    (ActivateAccountDto Account) : IRequest<Response<string>>;
