using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record AuthenticateUserCommand
    (AuthenticateUserDto Account) : IRequest<Response<string>>;
