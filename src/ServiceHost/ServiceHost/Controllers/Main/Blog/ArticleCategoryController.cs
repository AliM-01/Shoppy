using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Site;
using BM.Application.ArticleCategory.Queries.SiteCategory;

namespace ServiceHost.Controllers.Main.Article;

[SwaggerTag("مقاله ها")]
public class ArticleCategoryController : BaseApiController
{
    [HttpGet(MainBlogEndpoints.ArticleCategory.GetArticleCategoryList)]
    [SwaggerOperation(Summary = "دریافت دسته بندی های مقالات", Tags = new[] { "ProductCategory" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<ArticleCategorySiteDto>), 200)]
    public async Task<ActionResult<IEnumerable<ArticleCategorySiteDto>>> GetArticleCategoryList(CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategorySiteListQuery(), cancellationToken);

        return SuccessResult(res);
    }

    [HttpGet(MainBlogEndpoints.ArticleCategory.GetArticleCategory)]
    [SwaggerOperation(Summary = "دریافت دسته بندی مقاله", Tags = new[] { "ArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ArticleCategoryDetailsQueryModel), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<ActionResult<ArticleCategoryDetailsQueryModel>> GetArticleCategory([FromQuery] FilterArticleCategorySiteDto filter,
        CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategoryWithArticlesSiteQuery(filter), cancellationToken);

        return SuccessResult(res);
    }
}