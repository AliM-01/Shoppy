using BM.Application.Contracts.Article.Commands;
using BM.Application.Contracts.Article.DTOs;
using BM.Application.Contracts.Article.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Blog;

[SwaggerTag("مدیریت مقالات")]
public class AdminArticleController : BaseAdminApiController
{
    #region Filter Articles

    [HttpGet(AdminBlogEndpoints.Article.FilterArticles)]
    [SwaggerOperation(Summary = "فیلتر  مقالات", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(ApiResult<FilterArticleDto>), 200)]
    public async Task<IActionResult> FilterArticles([FromQuery] FilterArticleDto filter, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new FilterArticlesQuery(filter), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Article Details

    [HttpGet(AdminBlogEndpoints.Article.GetArticleDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult<EditArticleDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetArticleDetails([FromRoute] string id, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleDetailsQuery(id), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Article 

    [HttpPost(AdminBlogEndpoints.Article.CreateArticle)]
    [SwaggerOperation(Summary = "ایجاد  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> CreateArticle([FromForm] CreateArticleDto createRequest)
    {
        var res = await Mediator.Send(new CreateArticleCommand(createRequest));

        return JsonApiResult.Created(res);
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

        return JsonApiResult.Success(res);
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

        return JsonApiResult.Success(res);
    }

    #endregion
}
