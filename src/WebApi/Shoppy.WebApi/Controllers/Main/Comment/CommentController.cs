using _01_Shoppy.Query.Contracts.Comment;
using CM.Application.Contracts.Comment.Commands;
using CM.Application.Contracts.Comment.DTOs;

namespace Shoppy.WebApi.Controllers.Main.Comment;

[SwaggerTag("کامنت ها")]
public class CommentController : BaseApiController
{
    #region Ctor 

    private readonly ICommentQuery _commentQuery;

    public CommentController(ICommentQuery commentQuery)
    {
        _commentQuery = Guard.Against.Null(commentQuery, nameof(_commentQuery)); ;
    }

    #endregion

    #region Get Record Comments By Id

    [HttpGet(MainCommentApiEndpoints.Comment.GetRecordCommentsById)]
    [SwaggerOperation(Summary = "دریافت کامنت های محصول/مقاله", Tags = new[] { "Comment" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetRecordCommentsById([FromRoute] long recordId)
    {
        var res = await _commentQuery.GetRecordCommentsById(recordId);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Category

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
