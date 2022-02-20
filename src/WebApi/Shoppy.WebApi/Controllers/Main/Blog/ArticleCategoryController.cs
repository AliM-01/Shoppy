using _01_Shoppy.Query.Models.Blog.ArticleCategory;
using _01_Shoppy.Query.Queries.ArticleCategory;

namespace Shoppy.WebApi.Controllers.Main.Article;

[SwaggerTag("مقاله ها")]
public class ArticleCategoryController : BaseApiController
{
    #region Get Article Category

    [HttpGet(MainBlogApiEndpoints.ArticleCategory.GetArticleCategory)]
    [SwaggerOperation(Summary = "دریافت دسته بندی مقاله", Tags = new[] { "ArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetArticleCategory([FromQuery] FilterArticleCategoryDetailsModel filter)
    {
        var res = await Mediator.Send(new GetArticleCategoryWithArticlesByQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion
}
