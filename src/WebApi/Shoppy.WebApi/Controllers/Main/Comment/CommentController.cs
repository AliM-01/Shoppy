using _01_Shoppy.Query.Queries.Comment;
using CM.Application.Contracts.Comment.Commands;
using CM.Application.Contracts.Comment.DTOs;

namespace Shoppy.WebApi.Controllers.Main.Comment;

[SwaggerTag("کامنت ها")]
public class CommentController : BaseApiController
{
    #region Get Record Comments By Id

    [HttpGet(MainCommentApiEndpoints.Comment.GetRecordCommentsById)]
    [SwaggerOperation(Summary = "دریافت کامنت های محصول/مقاله", Tags = new[] { "Comment" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetRecordCommentsById([FromRoute] string recordId)
    {
        var res = await Mediator.Send(new GetRecordCommentsByIdQuery(recordId));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Add Comment

    [HttpPost(MainCommentApiEndpoints.Comment.AddComment)]
    [SwaggerOperation(Summary = "ایجاد کامنت", Tags = new[] { "Comment" })]
    [SwaggerResponse(201, "success : created")]
    public async Task<IActionResult> AddComment([FromForm] AddCommentDto addRequest)
    {
        var res = await Mediator.Send(new AddCommentCommand(addRequest));

        return JsonApiResult.Created(res);
    }

    #endregion
}
