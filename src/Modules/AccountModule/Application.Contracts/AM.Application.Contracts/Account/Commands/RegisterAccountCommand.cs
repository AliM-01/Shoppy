using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record RegisterAccountCommand
    (RegisterAccountDto Account) : IRequest<Response<string>>;
