using SM.Application.Contracts.ProductCategory.Commands;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت دسته بندی محصولات")]
public class AdminProductCategoryController : BaseAdminApiController
{
    #region Get ProductCategories List

    [HttpGet(AdminShopEndpoints.ProductCategory.GetProductCategoriesList)]
    [SwaggerOperation(Summary = "دریافت لیست دسته بندی محصولات", Tags = new[] { "AdminProductCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult<List<ProductCategoryForSelectListDto>>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetProductCategoriesList()
    {
        var res = await Mediator.Send(new GetProductCategoriesListQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Filter Product Categories

    [HttpGet(AdminShopEndpoints.ProductCategory.FilterProductCategories)]
    [SwaggerOperation(Summary = "فیلتر دسته بندی محصولات", Tags = new[] { "AdminProductCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult<FilterProductCategoryDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> FilterProductCategories([FromQuery] FilterProductCategoryDto filter)
    {
        var res = await Mediator.Send(new FilterProductCategoriesQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get ProductCategory Details

    [HttpGet(AdminShopEndpoints.ProductCategory.GetProductCategoryDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات دسته بندی محصول", Tags = new[] { "AdminProductCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult<EditProductCategoryDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetProductCategoryDetails([FromRoute] string id)
    {
        var res = await Mediator.Send(new GetProductCategoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Category

    [HttpPost(AdminShopEndpoints.ProductCategory.CreateProductCategory)]
    [SwaggerOperation(Summary = "ایجاد دسته بندی محصول", Tags = new[] { "AdminProductCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [ProducesResponseType(typeof(ApiResult), 201)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    public async Task<IActionResult> CreateProductCategory([FromForm] CreateProductCategoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCategoryCommand(createRequest));

        return JsonApiResult.Created(res);
    }

    #endregion

    #region Edit Product Category

    [HttpPut(AdminShopEndpoints.ProductCategory.EditProductCategory)]
    [SwaggerOperation(Summary = "ویرایش دسته بندی محصول", Tags = new[] { "AdminProductCategory" })]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 400)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> EditProductCategory([FromForm] EditProductCategoryDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCategoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Product Category

    [HttpDelete(AdminShopEndpoints.ProductCategory.DeleteProductCategory)]
    [SwaggerOperation(Summary = "حذف دسته بندی محصول", Tags = new[] { "AdminProductCategory" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] string id)
    {
        var res = await Mediator.Send(new DeleteProductCategoryCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}