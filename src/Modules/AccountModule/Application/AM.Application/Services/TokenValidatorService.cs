﻿using _0_Framework.Api;
using AM.Application.Contracts.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AM.Application.Services;

public class TokenValidatorService : ITokenValidatorService
{
    #region ctor

    private readonly UserManager<Domain.Account.Account> _userManager;
    private readonly ITokenStoreService _tokenStoreService;

    public TokenValidatorService(UserManager<Domain.Account.Account> userManager, ITokenStoreService tokenStoreService)
    {
        _userManager = userManager;
        _tokenStoreService = tokenStoreService;
    }

    #endregion

    #region ValidateAsync

    public async Task ValidateAsync(TokenValidatedContext context)
    {
        var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;

        if (claimsIdentity?.Claims is null || !claimsIdentity.Claims.Any())
        {
            context.Fail("This is not our issued token. It has no claims.");
            return;
        }

        var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
        if (serialNumberClaim is null)
        {
            context.Fail("This is not our issued token. It has no serial.");
            return;
        }

        var userId = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Fail("This is not our issued token. It has no user-id.");
            return;
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null || user.SerialNumber != serialNumberClaim.Value || !user.EmailConfirmed)
        {
            context.Fail("This token is expired. Please login again.");
        }

        if (!(context.SecurityToken is JwtSecurityToken accessToken) || string.IsNullOrWhiteSpace(accessToken.RawData) ||
            !await _tokenStoreService.IsValidToken(accessToken.RawData, userId))
        {
            context.Fail("This token is not in our database.");
            return;
        }


        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";
        var response = CustonJsonConverter.Serialize(new
        {
            status = "success",
            message = "احراز هویت با موفقیت انجام شد"
        });
        await context.Response.WriteAsync(response);
    }

    #endregion


}
