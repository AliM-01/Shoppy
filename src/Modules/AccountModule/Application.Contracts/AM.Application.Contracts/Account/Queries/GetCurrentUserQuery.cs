using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Queries;

public record GetCurrentUserQuery
    (string UserId) : IRequest<ApiResult<AccountDto>>;
