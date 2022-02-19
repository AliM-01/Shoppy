using BM.Application.Contracts.ArticleCategory.Commands;
using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Blog;

[SwaggerTag("مدیریت دسته بندی مقالات")]
public class AdminArticleCategoryController : BaseApiController
{
    #region Get Article Categories Select List

    [HttpGet(AdminBlogBlogApiEndpoints.ArticleCategory.GetArticleCategoriesSelectList)]
    [SwaggerOperation(Summary = "دریافت لیست دسته بندی مقالات", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetArticleCategoriesSelectList()
    {
        var res = await Mediator.Send(new GetArticleCategoriesSelectListQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Filter Article Categories

    [HttpGet(AdminBlogBlogApiEndpoints.ArticleCategory.FilterArticleCategories)]
    [SwaggerOperation(Summary = "فیلتر دسته بندی مقالات", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterArticleCategories([FromQuery] FilterArticleCategoryDto filter)
    {
        var res = await Mediator.Send(new FilterArticleCategoriesQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get ArticleCategory Details

    [HttpGet(AdminBlogBlogApiEndpoints.ArticleCategory.GetArticleCategoryDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetArticleCategoryDetails([FromRoute] Guid id)
    {
        var res = await Mediator.Send(new GetArticleCategoryDetailsQuery(id.ToString()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Article Category

    [HttpPost(AdminBlogBlogApiEndpoints.ArticleCategory.CreateArticleCategory)]
    [SwaggerOperation(Summary = "ایجاد دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    public async Task<IActionResult> CreateArticleCategory([FromForm] CreateArticleCategoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateArticleCategoryCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Article Category

    [HttpPut(AdminBlogBlogApiEndpoints.ArticleCategory.EditArticleCategory)]
    [SwaggerOperation(Summary = "ویرایش دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditArticleCategory([FromForm] EditArticleCategoryDto editRequest)
    {
        var res = await Mediator.Send(new EditArticleCategoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Article Category

    [HttpDelete(AdminBlogBlogApiEndpoints.ArticleCategory.DeleteArticleCategory)]
    [SwaggerOperation(Summary = "حذف دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DeleteArticleCategory([FromRoute] Guid id)
    {
        var res = await Mediator.Send(new DeleteArticleCategoryCommand(id.ToString()));

        return JsonApiResult.Success(res);
    }

    #endregion
}
