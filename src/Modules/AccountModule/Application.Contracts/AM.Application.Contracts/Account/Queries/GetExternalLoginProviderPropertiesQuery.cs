using Microsoft.AspNetCore.Authentication;

namespace AM.Application.Contracts.Account.Queries;
public record GetExternalLoginProviderPropertiesQuery(string Provider, string ReturnUrl) : IRequest<AuthenticationProperties>;
