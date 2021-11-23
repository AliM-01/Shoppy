using SM.Application.Contracts.ProductCategory.Commands;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace Shoppy.Admin.WebApi.Controllers;
public class AdminProductCategoryController : BaseApiController
{
    /// <summary>
    ///    دریافت لیست دسته بندی محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.AdminProductCategory.GetProductCategoriesList)]
    public async Task<IActionResult> GetProductCategoriesList()
    {
        var res = await Mediator.Send(new GetProductCategoriesListQuery());

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    فیلتر دسته بندی محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.AdminProductCategory.FilterProductCategories)]
    public async Task<IActionResult> FilterProductCategories([FromQuery] FilterProductCategoryDto filter)
    {
        var res = await Mediator.Send(new FilterProductCategoriesQuery(filter));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    دریافت جزییات دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.AdminProductCategory.GetProductCategoryDetails)]
    public async Task<IActionResult> GetProductCategoryDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductCategoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ایجاد دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(ApiEndpoints.AdminProductCategory.CreateProductCategory)]
    public async Task<IActionResult> CreateProductCategory([FromForm] CreateProductCategoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCategoryCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ویرایش دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(ApiEndpoints.AdminProductCategory.EditProductCategory)]
    public async Task<IActionResult> EditProductCategory([FromForm] EditProductCategoryDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCategoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    حذف دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(ApiEndpoints.AdminProductCategory.DeleteProductCategory)]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteProductCategoryCommand(id));

        return JsonApiResult.Success(res);
    }
}