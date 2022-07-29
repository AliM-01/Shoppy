using AM.Application.Contracts.Account.DTOs;
using AM.Application.Contracts.Account.Queries;

namespace Shoppy.WebApi.Controllers.User;

[SwaggerTag("پروفایل کاربر")]
public class UserProfileController : BaseUserApiController
{
    #region Get Profile

    [HttpGet(UserProfileEndpoints.Profile.GetProfile)]
    [SwaggerOperation(Summary = "دریافت پروفایل کاربر", Tags = new[] { "UserProfile" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(204, "no-content")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(UserProfileDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 401)]
    public async Task<IActionResult> GetProfile()
    {
        var res = await Mediator.Send(new GetUserProfileQuery(User.GetUserId()));

        return SuccessResult(res);
    }

    #endregion
}