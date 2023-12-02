using BM.Application.Article.Commands;
using BM.Application.Article.DTOs;
using BM.Application.Article.Queries;
using BM.Application.Article.Queries.Admin;

namespace ServiceHost.Controllers.Admin.Blog;

[SwaggerTag("مدیریت مقالات")]
public class AdminArticleController : BaseAdminApiController
{
    #region Filter Articles

    [HttpGet(AdminBlogEndpoints.Article.FilterArticles)]
    [SwaggerOperation(Summary = "فیلتر  مقالات", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(FilterArticleDto), 200)]
    public async Task<IActionResult> FilterArticles([FromQuery] FilterArticleDto filter,
        CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new FilterArticlesAdminQuery(filter), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region Get Article Details

    [HttpGet(AdminBlogEndpoints.Article.GetArticleDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(EditArticleDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetArticleDetails([FromRoute] string id, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleDetailsAdminQuery(id), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region Create Article

    [HttpPost(AdminBlogEndpoints.Article.CreateArticle)]
    [SwaggerOperation(Summary = "ایجاد  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> CreateArticle([FromForm] CreateArticleRequest createRequest)
    {
        var res = await Mediator.Send(new CreateArticleCommand(createRequest));

        return CreatedResult(res);
    }

    #endregion

    #region Edit Article

    [HttpPut(AdminBlogEndpoints.Article.EditArticle)]
    [SwaggerOperation(Summary = "ویرایش  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditArticle([FromForm] EditArticleDto editRequest)
    {
        var res = await Mediator.Send(new EditArticleCommand(editRequest));

        return SuccessResult(res);
    }

    #endregion

    #region Delete Article

    [HttpDelete(AdminBlogEndpoints.Article.DeleteArticle)]
    [SwaggerOperation(Summary = "حذف  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> DeleteArticle([FromRoute] string id)
    {
        var res = await Mediator.Send(new DeleteArticleCommand(id));

        return SuccessResult(res);
    }

    #endregion
}