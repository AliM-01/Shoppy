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
    private readonly IEmailSenderService _emailSender;
    private readonly IViewRenderService _viewRenderService;
    private readonly RoleManager<AccountRole> _roleManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ITokenStoreService tokenStoreService,
                             IEmailSenderService emailSender,
                             IViewRenderService viewRenderService,
                             ILogger<AccountController> logger,
                             RoleManager<AccountRole> roleManager)
    {
        _tokenStoreService = Guard.Against.Null(tokenStoreService, nameof(_tokenStoreService));
        _emailSender = Guard.Against.Null(emailSender, nameof(_emailSender));
        _viewRenderService = Guard.Against.Null(viewRenderService, nameof(_viewRenderService));
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
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> Register([FromForm] RegisterAccountRequestDto register, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new RegisterAccountCommand(register), cancellationToken);

        res.Data.CallBackUrl = $"{MainAccountEndpoints.Account.ConfirmEmail}?tId={res.Data.Token}";

        string emailBody = _viewRenderService.RenderToString("_ActivateEmail.cshtml", res.Data.CallBackUrl);

        _emailSender.SendEmail(res.Data.UserEmail, res.Data.UserFullName, "فعالسازی حساب", emailBody);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Login

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.Login)]
    [SwaggerOperation(Summary = "ورود به حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "not active")]
    [ProducesResponseType(typeof(ApiResult<AuthenticateUserResponseDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> Login([FromForm] AuthenticateUserRequestDto login, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new AuthenticateUserCommand(login), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region ConfirmEmail

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.ConfirmEmail)]
    [SwaggerOperation(Summary = "فعالسازی حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> ConfirmEmail([FromQuery(Name = "uId")] string userId,
                                                  [FromQuery(Name = "tId")] string activationToken,
                                                  CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var res = await Mediator.Send(new ActivateAccountCommand(new ActivateAccountRequestDto(userId,
                                                                                               activationToken)), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region RefreshToken

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.RefreshToken)]
    [SwaggerOperation(Summary = "refresh token", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not found")]
    [ProducesResponseType(typeof(ApiResult<AuthenticateUserResponseDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
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
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
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
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
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
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 401)]
    public IActionResult IsAuthenticated(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!(this.User.Identity.IsAuthenticated))
            return JsonApiResult.Unauthorized();

        return JsonApiResult.Success(ApiResponse.Success("احراز هویت با موفقیت انجام شد"));
    }

    #endregion

    #region IsInRole

    [AllowAnonymous]
    [HttpGet(MainAccountEndpoints.Account.IsInRole)]
    [SwaggerOperation(Summary = "Is InRole", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 401)]
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

        return JsonApiResult.Success(ApiResponse.Success("احراز هویت با موفقیت انجام شد"));
    }

    #endregion

    #region GetCurrentUser

    [Authorize]
    [HttpGet(MainAccountEndpoints.Account.GetCurrentUser)]
    [SwaggerOperation(Summary = "Get CurrentUser", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(401, "un-authorized")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 401)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new GetCurrentUserQuery(User.GetUserId()), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion
}
