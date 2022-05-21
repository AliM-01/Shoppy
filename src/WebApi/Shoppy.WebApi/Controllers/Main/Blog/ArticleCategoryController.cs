using _01_Shoppy.Query.Models.Blog.ArticleCategory;
using _01_Shoppy.Query.Queries.ArticleCategory;

namespace Shoppy.WebApi.Controllers.Main.Article;

[SwaggerTag("مقاله ها")]
public class ArticleCategoryController : BaseApiController
{
    #region Get ArticleCategory List

    [HttpGet(MainBlogEndpoints.ArticleCategory.GetArticleCategoryList)]
    [SwaggerOperation(Summary = "دریافت دسته بندی های مقالات", Tags = new[] { "ProductCategory" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<ArticleCategoryQueryModel>), 200)]
    public async Task<IActionResult> GetArticleCategoryList(CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategoryListQuery(), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region Get Article Category

    [HttpGet(MainBlogEndpoints.ArticleCategory.GetArticleCategory)]
    [SwaggerOperation(Summary = "دریافت دسته بندی مقاله", Tags = new[] { "ArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ArticleCategoryDetailsQueryModel), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetArticleCategory([FromQuery] FilterArticleCategoryDetailsModel filter, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategoryWithArticlesByQuery(filter), cancellationToken);

        return SuccessResult(res);
    }

    #endregion
}
