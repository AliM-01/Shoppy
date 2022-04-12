namespace AM.Application.Contracts.Account.Commands;

public record ActivateAccountByAdminCommand
    (string UserId) : IRequest<ApiResult>;
