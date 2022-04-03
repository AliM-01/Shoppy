using BM.Application.Contracts.ArticleCategory.Commands;
using BM.Application.Contracts.ArticleCategory.DTOs;
using BM.Application.Contracts.ArticleCategory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Blog;

[SwaggerTag("مدیریت دسته بندی مقالات")]
public class AdminArticleCategoryController : BaseAdminApiController
{
    #region Get Article Categories Select List

    [HttpGet(AdminBlogEndpoints.ArticleCategory.GetArticleCategoriesSelectList)]
    [SwaggerOperation(Summary = "دریافت لیست دسته بندی مقالات", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<List<ArticleCategoryForSelectListDto>>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> GetArticleCategoriesSelectList(CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategoriesSelectListQuery(), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Filter Article Categories

    [HttpGet(AdminBlogEndpoints.ArticleCategory.FilterArticleCategories)]
    [SwaggerOperation(Summary = "فیلتر دسته بندی مقالات", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<FilterArticleCategoryDto>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> FilterArticleCategories([FromQuery] FilterArticleCategoryDto filter, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new FilterArticleCategoriesQuery(filter), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get ArticleCategory Details

    [HttpGet(AdminBlogEndpoints.ArticleCategory.GetArticleCategoryDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<EditArticleCategoryDto>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> GetArticleCategoryDetails([FromRoute] string id, CancellationToken cancellationToken)
    {
        var res = await Mediator.Send(new GetArticleCategoryDetailsQuery(id), cancellationToken);

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Article Category

    [HttpPost(AdminBlogEndpoints.ArticleCategory.CreateArticleCategory)]
    [SwaggerOperation(Summary = "ایجاد دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [ProducesResponseType(typeof(Response<string>), 201)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> CreateArticleCategory([FromForm] CreateArticleCategoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateArticleCategoryCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Article Category

    [HttpPut(AdminBlogEndpoints.ArticleCategory.EditArticleCategory)]
    [SwaggerOperation(Summary = "ویرایش دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<string>), 201)]
    [ProducesResponseType(typeof(Response<string>), 400)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> EditArticleCategory([FromForm] EditArticleCategoryDto editRequest)
    {
        var res = await Mediator.Send(new EditArticleCategoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Article Category

    [HttpDelete(AdminBlogEndpoints.ArticleCategory.DeleteArticleCategory)]
    [SwaggerOperation(Summary = "حذف دسته بندی مقاله", Tags = new[] { "AdminArticleCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<string>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> DeleteArticleCategory([FromRoute] string id)
    {
        var res = await Mediator.Send(new DeleteArticleCategoryCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}
