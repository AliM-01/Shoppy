﻿using _01_Shoppy.Query.Queries.Article;

namespace Shoppy.WebApi.Controllers.Main.Article;

[SwaggerTag("مقاله ها")]
public class ArticleController : BaseApiController
{
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
