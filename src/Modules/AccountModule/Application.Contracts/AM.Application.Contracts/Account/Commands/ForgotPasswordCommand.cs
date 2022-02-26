using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record ForgotPasswordCommand
    (ForgotPasswordDto Password) : IRequest<Response<string>>;
