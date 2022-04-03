using _01_Shoppy.Query.Models.Comment;
using _01_Shoppy.Query.Queries.Comment;
using CM.Application.Contracts.Comment.Commands;
using CM.Application.Contracts.Comment.DTOs;

namespace Shoppy.WebApi.Controllers.Main.Comment;

[SwaggerTag("کامنت ها")]
public class CommentController : BaseApiController
{
    #region Get Record Comments By Id

    [HttpGet(MainCommentEndpoints.Comment.GetRecordCommentsById)]
    [SwaggerOperation(Summary = "دریافت کامنت های محصول/مقاله", Tags = new[] { "Comment" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<List<CommentQueryModel>>), 404)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> GetRecordCommentsById([FromRoute] string recordId, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetRecordCommentsByIdQuery(recordId), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Add Comment

    [HttpPost(MainCommentEndpoints.Comment.AddComment)]
    [SwaggerOperation(Summary = "ایجاد کامنت", Tags = new[] { "Comment" })]
    [SwaggerResponse(201, "success : created")]
    [ProducesResponseType(typeof(Response<string>), 201)]
    public async Task<IActionResult> AddComment([FromForm] AddCommentDto addRequest)
    {
        var res = await Mediator.Send(new AddCommentCommand(addRequest));

        return JsonApiResult.Created(res);
    }

    #endregion
}
