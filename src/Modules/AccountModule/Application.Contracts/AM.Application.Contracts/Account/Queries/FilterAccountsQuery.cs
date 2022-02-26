using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Queries;

public record FilterAccountsQuery
    (FilterAccountDto Filter) : IRequest<Response<FilterAccountDto>>;
