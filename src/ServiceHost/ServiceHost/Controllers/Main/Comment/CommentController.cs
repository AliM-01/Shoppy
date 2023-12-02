using CM.Application.Comment.DTOs;
using CM.Application.Comment.Commands;
using CM.Application.Comment.Queries;

namespace ServiceHost.Controllers.Main.Comment;

[SwaggerTag("کامنت ها")]
public class CommentController : BaseApiController
{
    #region Get Record Comments By Id

    [HttpGet(MainCommentEndpoints.Comment.GetRecordCommentsById)]
    [SwaggerOperation(Summary = "دریافت کامنت های محصول/مقاله", Tags = new[] { "Comment" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(IEnumerable<SiteCommentDto>), 404)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetRecordCommentsById([FromRoute] string recordId,
        CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetRecordCommentsByIdQuery(recordId), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region Add Comment

    [HttpPost(MainCommentEndpoints.Comment.AddComment)]
    [SwaggerOperation(Summary = "ایجاد کامنت", Tags = new[] { "Comment" })]
    [SwaggerResponse(201, "success : created")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    public async Task<IActionResult> AddComment([FromForm] AddCommentDto addRequest)
    {
        var res = await Mediator.Send(new AddCommentCommand(addRequest));

        return CreatedResult(res);
    }

    #endregion
}