using _01_Shoppy.Query.Models.Blog.Article;
using _01_Shoppy.Query.Queries.Article;
using _01_Shoppy.Query.Queries.Blog.Article;

namespace Shoppy.WebApi.Controllers.Main.Article;

[SwaggerTag("مقاله ها")]
public class ArticleController : BaseApiController
{
    #region Search

    [HttpGet(MainBlogEndpoints.Article.Search)]
    [SwaggerOperation(Summary = "جستجو", Tags = new[] { "Article" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(400, "error : no data with requested filter")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<SearchArticleQueryModel>), 200)]
    [ProducesResponseType(typeof(Response<string>), 400)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> Search([FromQuery] SearchArticleQueryModel search, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new SearchArticleQuery(search), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Article Details

    [HttpGet(MainBlogEndpoints.Article.GetArticleDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات مقاله", Tags = new[] { "Article" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<ArticleDetailsQueryModel>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> GetArticleDetails([FromRoute] string slug, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleDetailsQuery(slug), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Latest Articles

    [HttpGet(MainBlogEndpoints.Article.GetLatestArticles)]
    [SwaggerOperation(Summary = "دریافت جدید ترین مقالات", Tags = new[] { "Article" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(Response<List<ArticleQueryModel>>), 200)]
    public async Task<IActionResult> GetLatestArticles(CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetLatestArticlesQuery(), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Related Articles

    [HttpGet(MainBlogEndpoints.Article.GetRelatedArticles)]
    [SwaggerOperation(Summary = "دریافت مقالات مرتبط", Tags = new[] { "Article" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(Response<List<ArticleQueryModel>>), 200)]
    public async Task<IActionResult> GetRelatedArticles([FromRoute] string categoryId, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetRelatedArticlesQuery(categoryId), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion
}
