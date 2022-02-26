using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record ChangePasswordCommand
    (ChangePasswordDto Password) : IRequest<Response<string>>;
