using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AM.Application.Contracts.Services;

public interface ITokenValidatorService
{
    Task ValidateAsync(TokenValidatedContext context);
}
