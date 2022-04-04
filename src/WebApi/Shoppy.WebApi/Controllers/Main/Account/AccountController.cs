using AM.Application.Contracts.Account.Commands;
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
    private readonly RoleManager<AccountRole> _roleManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ITokenStoreService tokenStoreService,
                             ILogger<AccountController> logger,
                             RoleManager<AccountRole> roleManager)
    {
        _tokenStoreService = Guard.Against.Null(tokenStoreService, nameof(_tokenStoreService));
        _logger = Guard.Against.Null(logger, nameof(_logger));
        _roleManager = Guard.Against.Null(roleManager, nameof(_roleManager));
    }

    #endregion

    #region Register

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.Register)]
    [SwaggerOperation(Summary = "ثبت نام", Tags = new[] { "Account" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "duplicate email")]
    [ProducesResponseType(typeof(Response<string>), 201)]
    [ProducesResponseType(typeof(Response<string>), 400)]
    public async Task<IActionResult> Register([FromForm] RegisterAccountDto register, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new RegisterAccountCommand(register), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Login

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.Login)]
    [SwaggerOperation(Summary = "ورود به حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "not active")]
    [ProducesResponseType(typeof(Response<AuthenticateUserResponseDto>), 200)]
    [ProducesResponseType(typeof(Response<string>), 400)]
    public async Task<IActionResult> Login([FromForm] AuthenticateUserRequestDto login, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new AuthenticateUserCommand(login), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region RefreshToken

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.RefreshToken)]
    [SwaggerOperation(Summary = "refresh token", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not found")]
    [ProducesResponseType(typeof(Response<AuthenticateUserResponseDto>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> RefreshToken([FromForm] RevokeRefreshTokenRequestDto token, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new RevokeRefreshTokenCommand(token), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region ForgotPassword

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.ForgotPassword)]
    [SwaggerOperation(Summary = "فراموشی رمز عبور", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<string>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto forgotPassword)
    {
        var res = await Mediator.Send(new ForgotPasswordCommand(forgotPassword));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Logout

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.Logout)]
    [SwaggerOperation(Summary = "خروج از حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<string>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
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
    [HttpGet(MainAccountEndpoints.Account.IsAuthenticated)]
    [SwaggerOperation(Summary = "Is Authenticated", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    [ProducesResponseType(typeof(Response<string>), 200)]
    [ProducesResponseType(typeof(Response<string>), 401)]
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
    [HttpGet(MainAccountEndpoints.Account.IsInRole)]
    [SwaggerOperation(Summary = "Is InRole", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    [ProducesResponseType(typeof(Response<string>), 200)]
    [ProducesResponseType(typeof(Response<string>), 401)]
    public async Task<IActionResult> IsInRole([FromQuery] string[] roles, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!(this.User.Identity.IsAuthenticated))
            return JsonApiResult.Unauthorized();

        foreach (var role in roles)
        {
            if (!(await _roleManager.RoleExistsAsync(role)))
                return JsonApiResult.Error("نقش مورد نظر وجود ندارد");

            if (!(this.User.IsInRole(role)))
                return JsonApiResult.Unauthorized();
        }

        return JsonApiResult.Success("احراز هویت با موفقیت انجام شد");
    }

    #endregion

    #region GetCurrentUser

    [Authorize]
    [HttpGet(MainAccountEndpoints.Account.GetCurrentUser)]
    [SwaggerOperation(Summary = "Get CurrentUser", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    [ProducesResponseType(typeof(Response<string>), 200)]
    [ProducesResponseType(typeof(Response<string>), 401)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new GetCurrentUserQuery(User.GetUserId()), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion
}
