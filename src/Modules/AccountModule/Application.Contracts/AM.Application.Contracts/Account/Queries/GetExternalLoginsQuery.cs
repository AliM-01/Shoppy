using Microsoft.AspNetCore.Authentication;

namespace AM.Application.Contracts.Account.Queries;
public record GetExternalLoginsQuery() : IRequest<ApiResult<IEnumerable<AuthenticationScheme>>>;