using SM.Application.Contracts.ProductCategory.Commands;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Shop;

[SwaggerTag("مدیریت دسته بندی محصولات")]
public class AdminProductCategoryController : BaseApiController
{
    #region Get ProductCategories List

    [HttpGet(AdminShopApiEndpoints.ProductCategory.GetProductCategoriesList)]
    [SwaggerOperation(Summary = "دریافت لیست دسته بندی محصولات")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductCategoriesList()
    {
        var res = await Mediator.Send(new GetProductCategoriesListQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Filter Product Categories

    [HttpGet(AdminShopApiEndpoints.ProductCategory.FilterProductCategories)]
    [SwaggerOperation(Summary = "فیلتر دسته بندی محصولات")]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterProductCategories([FromQuery] FilterProductCategoryDto filter)
    {
        var res = await Mediator.Send(new FilterProductCategoriesQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get ProductCategory Details

    [HttpGet(AdminShopApiEndpoints.ProductCategory.GetProductCategoryDetails)]
    [SwaggerOperation(Summary = "دریافت جزییات دسته بندی محصول")]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> GetProductCategoryDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductCategoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Create Product Category

    [HttpPost(AdminShopApiEndpoints.ProductCategory.CreateProductCategory)]
    [SwaggerOperation(Summary = "ایجاد دسته بندی محصول")]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    public async Task<IActionResult> CreateProductCategory([FromForm] CreateProductCategoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCategoryCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Edit Product Category

    [HttpPut(AdminShopApiEndpoints.ProductCategory.EditProductCategory)]
    [SwaggerOperation(Summary = "ویرایش دسته بندی محصول")]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(400, "error : title is duplicated")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> EditProductCategory([FromForm] EditProductCategoryDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCategoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Delete Product Category

    [HttpDelete(AdminShopApiEndpoints.ProductCategory.DeleteProductCategory)]
    [SwaggerOperation(Summary = "حذف دسته بندی محصول")]
    [SwaggerResponse(201, "success : created")]
    [SwaggerResponse(404, "not-found")]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteProductCategoryCommand(id));

        return JsonApiResult.Success(res);
    }

    #endregion
}