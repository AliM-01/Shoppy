using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Queries;

public record GetAccountDetailsQuery
    (string UserId) : IRequest<ApiResult<EditAccountDto>>;
