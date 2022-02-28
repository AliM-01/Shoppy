using AM.Application.Contracts.Account.Commands;
using AM.Application.Contracts.Account.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Shoppy.WebApi.Controllers.Main.Account;

[AllowAnonymous]
[SwaggerTag("احراز هویت")]
public class AccountController : BaseApiController
{
    #region Register

    [HttpGet(MainAccountApiEndpoints.Account.Register)]
    [SwaggerOperation(Summary = "ثبت نام", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "duplicate email")]
    public async Task<IActionResult> Register([FromForm] RegisterAccountDto register)
    {
        var res = await Mediator.Send(new RegisterAccountCommand(register));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Login

    [HttpGet(MainAccountApiEndpoints.Account.Login)]
    [SwaggerOperation(Summary = "ورود به حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "not active")]
    public async Task<IActionResult> Login([FromForm] AuthenticateUserRequestDto Login)
    {
        var res = await Mediator.Send(new AuthenticateUserCommand(Login));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region RefreshToken

    [HttpGet(MainAccountApiEndpoints.Account.RefreshToken)]
    [SwaggerOperation(Summary = "refresh token", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not found")]
    public async Task<IActionResult> RefreshToken([FromBody] RevokeRefreshTokenRequestDto Token)
    {
        var res = await Mediator.Send(new RevokeRefreshTokenCommand(Token));

        return JsonApiResult.Success(res);
    }

    #endregion
}
