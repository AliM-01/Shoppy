namespace AM.Application.Contracts.Account.Commands;

public record DeActivateAccountCommand
    (string AccountId) : IRequest<ApiResult>;
