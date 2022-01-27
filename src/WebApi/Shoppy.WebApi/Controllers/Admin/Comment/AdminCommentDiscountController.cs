using CM.Application.Contracts.Comment.Commands;
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

    #region Confirm Comment

    /// <summary>
    ///    تایید کامنت
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminCommentApiEndpoints.Comment.ConfirmComment)]
    public async Task<IActionResult> ConfirmComment([FromRoute] long id)
    {
        var res = await Mediator.Send(new ConfirmCommentCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Cancel Comment

    /// <summary>
    ///    حذف کامنت
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(AdminCommentApiEndpoints.Comment.CancelComment)]
    public async Task<IActionResult> CancelComment([FromRoute] long id)
    {
        var res = await Mediator.Send(new CancelCommentCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

}
