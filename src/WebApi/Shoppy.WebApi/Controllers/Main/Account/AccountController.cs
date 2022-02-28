using AM.Application.Contracts.Account.Commands;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Shoppy.WebApi.Controllers.Main.Account;

[AllowAnonymous]
[SwaggerTag("احراز هویت")]
public class AccountController : BaseApiController
{
    #region ctor

    private readonly ITokenStoreService _tokenStoreService;

    public AccountController(ITokenStoreService tokenStoreService)
    {
        _tokenStoreService = tokenStoreService;
    }

    #endregion

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
    public async Task<IActionResult> Login([FromForm] AuthenticateUserRequestDto login)
    {
        var res = await Mediator.Send(new AuthenticateUserCommand(login));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region RefreshToken

    [HttpGet(MainAccountApiEndpoints.Account.RefreshToken)]
    [SwaggerOperation(Summary = "refresh token", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not found")]
    public async Task<IActionResult> RefreshToken([FromBody] RevokeRefreshTokenRequestDto token)
    {
        var res = await Mediator.Send(new RevokeRefreshTokenCommand(token));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region ForgotPassword

    [HttpGet(MainAccountApiEndpoints.Account.ForgotPassword)]
    [SwaggerOperation(Summary = "فراموشی رمز عبور", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto forgotPassword)
    {
        var res = await Mediator.Send(new ForgotPasswordCommand(forgotPassword));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Logout

    [HttpGet(MainAccountApiEndpoints.Account.Logout)]
    [SwaggerOperation(Summary = "خروج از حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> Logout([FromBody] RevokeRefreshTokenRequestDto token)
    {
        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

        await _tokenStoreService.RevokeUserBearerTokens(userId, token.RefreshToken);

        return JsonApiResult.Success();
    }

    #endregion

    #region IsAuthenticated

    [HttpGet(MainAccountApiEndpoints.Account.IsAuthenticated)]
    [SwaggerOperation(Summary = "Is Authenticated", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    public IActionResult IsAuthenticated()
    {
        bool isAuth = this.User.Identity.IsAuthenticated;

        if (isAuth)
            return JsonApiResult.Success();

        return JsonApiResult.Unauthorized();
    }

    #endregion
}
