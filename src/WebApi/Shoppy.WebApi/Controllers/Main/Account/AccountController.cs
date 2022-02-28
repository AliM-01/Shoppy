using AM.Application.Contracts.Account.Commands;
using AM.Application.Contracts.Account.DTOs;

namespace Shoppy.WebApi.Controllers.Main.Account;

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
}
