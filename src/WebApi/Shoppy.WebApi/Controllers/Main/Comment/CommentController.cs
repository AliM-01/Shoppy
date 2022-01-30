using CM.Application.Contracts.Comment.Commands;
using CM.Application.Contracts.Comment.DTOs;

namespace Shoppy.WebApi.Controllers.Main.Comment;

[SwaggerTag("کامنت ها")]
public class CommentController : BaseApiController
{
    #region Create Product Category

    [HttpPost(MainCommentApiEndpoints.Comment.AddComment)]
    [SwaggerOperation(Summary = "ایجاد کامنت")]
    [SwaggerResponse(201, "success : created")]
    public async Task<IActionResult> AddComment([FromForm] AddCommentDto addRequest)
    {
        var res = await Mediator.Send(new AddCommentCommand(addRequest));

        return JsonApiResult.Created(res);
    }

    #endregion
}
