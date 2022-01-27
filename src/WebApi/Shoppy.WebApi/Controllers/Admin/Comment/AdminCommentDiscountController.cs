using CM.Application.Contracts.Comment.DTOs;
using CM.Application.Contracts.Inventory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Comment;

public class AdminCommentDiscountController : BaseApiController
{
    #region Filter Comment

    /// <summary>
    ///    فیلتر کامنت ها
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(AdminCommentApiEndpoints.Comment.FilterComments)]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterCommentDto filter)
    {
        var res = await Mediator.Send(new FilterCommentsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

}
