﻿using AM.Application.Contracts.Account.Commands;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Account.Queries;
using AM.Application.Contracts.Services;
using AM.Domain.Account;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Shoppy.WebApi.Controllers.Main.Account;

[SwaggerTag("احراز هویت")]
public class AccountController : BaseApiController
{
    #region ctor

    private readonly ITokenStoreService _tokenStoreService;
    private readonly UserManager<AM.Domain.Account.Account> _userManager;
    private readonly RoleManager<AccountRole> _roleManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ITokenStoreService tokenStoreService,
                             ILogger<AccountController> logger,
                             RoleManager<AccountRole> roleManager,
                             UserManager<AM.Domain.Account.Account> userManager)
    {
        _tokenStoreService = Guard.Against.Null(tokenStoreService, nameof(_tokenStoreService));
        _logger = Guard.Against.Null(logger, nameof(_logger));
        _roleManager = Guard.Against.Null(roleManager, nameof(_roleManager));
        _userManager = Guard.Against.Null(userManager, nameof(_userManager));
    }

    #endregion

    #region Register

    [AllowAnonymous]
    [HttpPost(MainAccountApiEndpoints.Account.Register)]
    [SwaggerOperation(Summary = "ثبت نام", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "duplicate email")]
    public async Task<IActionResult> Register([FromForm] RegisterAccountDto register, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new RegisterAccountCommand(register), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Login

    [AllowAnonymous]
    [HttpPost(MainAccountApiEndpoints.Account.Login)]
    [SwaggerOperation(Summary = "ورود به حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "not active")]
    public async Task<IActionResult> Login([FromForm] AuthenticateUserRequestDto login, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new AuthenticateUserCommand(login));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region RefreshToken

    [AllowAnonymous]
    [HttpPost(MainAccountApiEndpoints.Account.RefreshToken)]
    [SwaggerOperation(Summary = "refresh token", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not found")]
    public async Task<IActionResult> RefreshToken([FromForm] RevokeRefreshTokenRequestDto token, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new RevokeRefreshTokenCommand(token), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region ForgotPassword

    [AllowAnonymous]
    [HttpPost(MainAccountApiEndpoints.Account.ForgotPassword)]
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

    [AllowAnonymous]
    [HttpPost(MainAccountApiEndpoints.Account.Logout)]
    [SwaggerOperation(Summary = "خروج از حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> Logout([FromForm] RevokeRefreshTokenRequestDto token)
    {
        var claimsIdentity = this.User.Identity as ClaimsIdentity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

        await _tokenStoreService.RevokeUserBearerTokens(userId, token.RefreshToken);

        return JsonApiResult.Success();
    }

    #endregion

    #region IsAuthenticated

    [AllowAnonymous]
    [HttpGet(MainAccountApiEndpoints.Account.IsAuthenticated)]
    [SwaggerOperation(Summary = "Is Authenticated", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    public IActionResult IsAuthenticated(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!(this.User.Identity.IsAuthenticated))
            return JsonApiResult.Unauthorized();

        return JsonApiResult.Success("احراز هویت با موفقیت انجام شد");
    }

    #endregion

    #region IsInRole

    [AllowAnonymous]
    [HttpGet(MainAccountApiEndpoints.Account.IsInRole)]
    [SwaggerOperation(Summary = "Is InRole", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    public async Task<IActionResult> IsInRole([FromQuery] string[] roles, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!(this.User.Identity.IsAuthenticated))
            return JsonApiResult.Unauthorized();

        var user = await _userManager.FindByIdAsync(User.GetUserId());

        foreach (var role in roles)
        {
            if (!(await _roleManager.RoleExistsAsync(role)))
                return JsonApiResult.Error("نقش مورد نظر وجود ندارد");

            if (!(await _userManager.IsInRoleAsync(user, role)))
                return JsonApiResult.Unauthorized();
        }

        return JsonApiResult.Success("احراز هویت با موفقیت انجام شد");
    }

    #endregion

    #region GetCurrentUser

    [Authorize]
    [HttpGet(MainAccountApiEndpoints.Account.GetCurrentUser)]
    [SwaggerOperation(Summary = "Get CurrentUser", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new GetCurrentUserQuery(User.GetUserId()), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion
}
