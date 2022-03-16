using AM.Application.Contracts.Account.Commands;
using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Account.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Blog;

[SwaggerTag("مدیریت کاربران")]
public class AdminAccountController : BaseAdminApiController
{
    #region Filter Accounts

    [HttpGet(AdminAccountApiEndpoints.Account.FilterAccounts)]
    [SwaggerOperation(Summary = "فیلتر  کاربران", Tags = new[] { "AdminAccount" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterAccounts([FromQuery] FilterAccountDto filter)
    {
        var res = await Mediator.Send(new FilterAccountsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Account Details

    [HttpGet(AdminAccountApiEndpoints.Account.GetAccountDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات  کاربر", Tags = new[] { "AdminAccount" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetAccountDetails([FromRoute] Guid id)
    {
        var res = await Mediator.Send(new GetAccountDetailsQuery(id.ToString()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Account 

    [HttpPut(AdminAccountApiEndpoints.Account.EditAccount)]
    [SwaggerOperation(Summary = "ویرایش  کاربر", Tags = new[] { "AdminAccount" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditAccount([FromForm] EditAccountDto editRequest)
    {
        var res = await Mediator.Send(new EditAccountCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Activate Account 

    [HttpPost(AdminAccountApiEndpoints.Account.ActivateAccount)]
    [SwaggerOperation(Summary = "فعال کردن حساب  کاربر", Tags = new[] { "AdminAccount" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> ActivateAccount([FromRoute] Guid id)
    {
        var res = await Mediator.Send(new ActivateAccountByAdminCommand(id.ToString()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region DeActivate Account 

    [HttpDelete(AdminAccountApiEndpoints.Account.DeActivateAccount)]
    [SwaggerOperation(Summary = "غیر فعال کردن حساب  کاربر", Tags = new[] { "AdminAccount" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DeActivateAccount([FromRoute] Guid id)
    {
        var res = await Mediator.Send(new DeActivateAccountCommand(id.ToString()));

        return JsonApiResult.Success(res);
    }

    #endregion
}
