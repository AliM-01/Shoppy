using BM.Application.ArticleCategory.Commands;
using BM.Application.ArticleCategory.Models;
using BM.Application.ArticleCategory.Models.Admin;
using BM.Application.ArticleCategory.Queries;
using BM.Application.ArticleCategory.Queries.Admin;

namespace ServiceHost.Controllers.Admin.Blog;

[SwaggerTag("مدیریت دسته بندی مقالات")]
public class AdminArticleCategoryController : BaseAdminApiController
{
    #region Get Article Categories Select List

    [HttpGet(AdminBlogEndpoints.ArticleCategory.GetArticleCategoriesSelectList)]
    [SwaggerOperation(Summary = "دریافت لیست دسته بندی مقالات", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(IEnumerable<ArticleCategoryForSelectListAdminDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetArticleCategoriesSelectList(CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategoriesSelectListAdminQuery(), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region Filter Article Categories

    [HttpGet(AdminBlogEndpoints.ArticleCategory.FilterArticleCategories)]
    [SwaggerOperation(Summary = "فیلتر دسته بندی مقالات", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(FilterArticleCategoryAdminDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> FilterArticleCategories([FromQuery] FilterArticleCategoryAdminDto filter,
        CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new FilterArticleCategoriesAdminQuery(filter), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region Get ArticleCategory Details

    [HttpGet(AdminBlogEndpoints.ArticleCategory.GetArticleCategoryDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(EditArticleCategoryAdminDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetArticleCategoryDetails([FromRoute] string id,
        CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategoryDetailsAdminQuery(id), cancellationToken);

        return SuccessResult(res);
    }

    #endregion

    #region Create Article Category

    [HttpPost(AdminBlogEndpoints.ArticleCategory.CreateArticleCategory)]
    [SwaggerOperation(Summary = "ایجاد دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> CreateArticleCategory([FromForm] CreateArticleCategoryAdminDto createRequest)
    {
        var res = await Mediator.Send(new CreateArticleCategoryCommand(createRequest));

        return CreatedResult(res);
    }

    #endregion

    #region Edit Article Category

    [HttpPut(AdminBlogEndpoints.ArticleCategory.EditArticleCategory)]
    [SwaggerOperation(Summary = "ویرایش دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditArticleCategory([FromForm] EditArticleCategoryAdminDto editRequest)
    {
        var res = await Mediator.Send(new EditArticleCategoryCommand(editRequest));

        return SuccessResult(res);
    }

    #endregion

    #region Delete Article Category

    [HttpDelete(AdminBlogEndpoints.ArticleCategory.DeleteArticleCategory)]
    [SwaggerOperation(Summary = "حذف دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> DeleteArticleCategory([FromRoute] string id)
    {
        var res = await Mediator.Send(new DeleteArticleCategoryCommand(id));

        return SuccessResult(res);
    }

    #endregion
}