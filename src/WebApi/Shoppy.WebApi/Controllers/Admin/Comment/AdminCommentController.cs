using CM.Application.Contracts.Comment.Commands;
using CM.Application.Contracts.Comment.DTOs;
using CM.Application.Contracts.Inventory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Comment;

[SwaggerTag("مدیریت کامنت ها")]
public class AdminCommentController : BaseApiController
{
    #region Filter Comment

    [HttpGet(AdminCommentApiEndpoints.Comment.FilterComments)]
    [SwaggerOperation(Summary = "فیلتر کامنت ها")]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterCommentDto filter)
    {
        var res = await Mediator.Send(new FilterCommentsQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Confirm Comment

    [HttpPost(AdminCommentApiEndpoints.Comment.ConfirmComment)]
    [SwaggerOperation(Summary = "تایید کامنت")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> ConfirmComment([FromRoute] long id)
    {
        var res = await Mediator.Send(new ConfirmCommentCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Cancel Comment

    [HttpPost(AdminCommentApiEndpoints.Comment.CancelComment)]
    [SwaggerOperation(Summary = "حذف کامنت")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> CancelComment([FromRoute] long id)
    {
        var res = await Mediator.Send(new CancelCommentCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion

}
