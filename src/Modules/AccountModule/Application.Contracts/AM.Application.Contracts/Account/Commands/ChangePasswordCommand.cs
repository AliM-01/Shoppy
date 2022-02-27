using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record ChangePasswordCommand
    (ChangePasswordDto Password, string UserId) : IRequest<Response<string>>;
