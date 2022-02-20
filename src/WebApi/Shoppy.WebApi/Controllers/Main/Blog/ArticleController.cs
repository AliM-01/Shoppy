using _01_Shoppy.Query.Queries.Article;
using _01_Shoppy.Query.Queries.Blog.Article;

namespace Shoppy.WebApi.Controllers.Main.Article;

[SwaggerTag("مقاله ها")]
public class ArticleController : BaseApiController
{
    #region Get Article Details

    [HttpGet(MainBlogApiEndpoints.Article.GetArticleDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات مقاله", Tags = new[] { "Article" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetArticleDetails([FromRoute] string slug)
    {
        var res = await Mediator.Send(new GetArticleDetailsQuery(slug));

        return JsonApiResult.Success(res);
    }

    #endregion
    #region Get Latest Articles

    [HttpGet(MainBlogApiEndpoints.Article.GetLatestArticles)]
    [SwaggerOperation(Summary = "دریافت جدید ترین مقالات", Tags = new[] { "Article" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetLatestArticles()
    {
        var res = await Mediator.Send(new GetLatestArticlesQuery());

        return JsonApiResult.Success(res);
    }

    #endregion
}
