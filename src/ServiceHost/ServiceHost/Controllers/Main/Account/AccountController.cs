using AM.Application.Account.Commands;
using AM.Application.Account.DTOs;
using AM.Application.Account.Queries;
using AM.Application.Services;
using AM.Domain.Account;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ServiceHost.Controllers.Main.Account;

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
    public async Task<IActionResult> Register([FromForm] RegisterAccountRequestDto register,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new RegisterAccountCommand(register), cancellationToken);

        #region send activation email

        var queryParams = new Dictionary<string, string>
        {
            { "tId", res.Token },
            { "uId", res.UserId }
        };

        string callBackUrl =
            QueryHelpers.AddQueryString($"https://localhost:5001/{MainAccountEndpoints.Account.ConfirmEmail}",
                queryParams);

        string emailBody = _viewRenderService.RenderToString("~/Shared/Views/_ActivateEmail.cshtml", callBackUrl);

        bool emailRes = await _emailSender.SendEmail(res.UserEmail, "فعالسازی حساب", emailBody);

        if (!emailRes)
            return ErrorResult();

        #endregion

        return SuccessResult("ثبت نام شما با موفقیت انجام شد. لطفا ایمیل خود را تایید کنید");
    }

    #endregion

    #region Login

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.Login)]
    [SwaggerOperation(Summary = "ورود به حساب", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "not active")]
    [ProducesResponseType(typeof(AuthenticateUserResponseDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> Login([FromForm] AuthenticateUserRequestDto login,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new AuthenticateUserCommand(login), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region GetExternalLoginsQuery

    [AllowAnonymous]
    [HttpGet(MainAccountEndpoints.Account.GetExternalLogins)]
    [SwaggerOperation(Summary = "Get ExternalLogins", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<IActionResult> GetExternalLogins(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new GetExternalLoginsQuery());

        return SuccessResult(res);
    }

    #endregion

    #region ExternalLogin

    [AllowAnonymous]
    [HttpGet(MainAccountEndpoints.Account.ExternalLogin)]
    [SwaggerOperation(Summary = "ExternalLogin", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<IActionResult> ExternalLogin([FromQuery] string provider, [FromQuery] string returnUrl,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new GetExternalLoginProviderPropertiesQuery(provider, returnUrl));

        return new ChallengeResult(provider, res);
    }

    #endregion

    #region ExternalLogin

    [AllowAnonymous]
    [HttpGet(MainAccountEndpoints.Account.ExternalLoginCallBack)]
    [SwaggerOperation(Summary = "ExternalLoginCallBack", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<IActionResult> ExternalLoginCallBack(CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new ExternalLoginCallbackCommand(), cancellationToken);

        #region welcome email

        string emailBody = _viewRenderService.RenderToString("~/Shared/Views/_Welcome.cshtml", "");

        await _emailSender.SendEmail(res.Email, "خوش آمدید", emailBody);

        #endregion

        return SuccessResult(res.TokenResult);
    }

    #endregion

    #region ConfirmEmail

    [AllowAnonymous]
    [HttpGet(MainAccountEndpoints.Account.ConfirmEmail)]
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

        return SuccessResult(res);
    }

    #endregion

    #region RefreshToken

    [AllowAnonymous]
    [HttpPost(MainAccountEndpoints.Account.RefreshToken)]
    [SwaggerOperation(Summary = "refresh token", Tags = new[] { "Account" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not found")]
    [ProducesResponseType(typeof(AuthenticateUserResponseDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> RefreshToken([FromForm] RevokeRefreshTokenRequestDto token,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var res = await Mediator.Send(new RevokeRefreshTokenCommand(token), cancellationToken);

        return SuccessResult(res);
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

        return SuccessResult(res);
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
        var claimsIdentity = User.Identity as ClaimsIdentity;
        string? userId = claimsIdentity.FindFirst(ClaimTypes.UserData)?.Value;

        await _tokenStoreService.RevokeUserBearerTokens(userId, token.RefreshToken);

        return SuccessResult();
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

        if (!User.Identity.IsAuthenticated)
            return UnauthorizedResult();

        return SuccessResult(ApiResponse.Success("احراز هویت با موفقیت انجام شد"));
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

        if (!User.Identity.IsAuthenticated)
            return UnauthorizedResult();

        foreach (string? role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                return ErrorResult("نقش مورد نظر وجود ندارد");

            if (!User.IsInRole(role))
                return UnauthorizedResult();
        }

        return SuccessResult(ApiResponse.Success("احراز هویت با موفقیت انجام شد"));
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

        return SuccessResult(res);
    }

    #endregion
}