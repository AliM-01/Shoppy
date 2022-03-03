using BM.Application.Contracts.Article.Commands;
using BM.Application.Contracts.Article.DTOs;
using BM.Application.Contracts.Article.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Blog;

[SwaggerTag("مدیریت مقالات")]
public class AdminArticleController : BaseAdminApiController
{
    #region Filter Articles

    [HttpGet(AdminBlogApiEndpoints.Article.FilterArticles)]
    [SwaggerOperation(Summary = "فیلتر  مقالات", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterArticles([FromQuery] FilterArticleDto filter)
    {
        var res = await Mediator.Send(new FilterArticlesQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Article Details

    [HttpGet(AdminBlogApiEndpoints.Article.GetArticleDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetArticleDetails([FromRoute] Guid id)
    {
        var res = await Mediator.Send(new GetArticleDetailsQuery(id.ToString()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Article 

    [HttpPost(AdminBlogApiEndpoints.Article.CreateArticle)]
    [SwaggerOperation(Summary = "ایجاد  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    public async Task<IActionResult> CreateArticle([FromForm] CreateArticleDto createRequest)
    {
        var res = await Mediator.Send(new CreateArticleCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Article 

    [HttpPut(AdminBlogApiEndpoints.Article.EditArticle)]
    [SwaggerOperation(Summary = "ویرایش  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditArticle([FromForm] EditArticleDto editRequest)
    {
        var res = await Mediator.Send(new EditArticleCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Article 

    [HttpDelete(AdminBlogApiEndpoints.Article.DeleteArticle)]
    [SwaggerOperation(Summary = "حذف  مقاله", Tags = new[] { "AdminArticle" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
    {
        var res = await Mediator.Send(new DeleteArticleCommand(id.ToString()));

        return JsonApiResult.Success(res);
    }

    #endregion
}
