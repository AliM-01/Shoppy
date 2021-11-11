using Microsoft.AspNetCore.Mvc;
using Shoppy.Admin.WebApi.Endpoints;
using SM.Application.Contracts.ProductCategory.Commands;
using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace Shoppy.Admin.WebApi.Controllers;
public class ProductCategoryController : BaseApiController
{
    /// <summary>
    ///    فیلتر دسته بندی محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.ProductCategory.FilterProductCategories)]
    public async Task<IActionResult> FilterProductCategories([FromQuery] FilterProductCategoryDto filter)
    {
        var res = await Mediator.Send(new FilterProductCategoriesQuery(filter));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    دریافت جزییات دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.ProductCategory.GetProductCategoryDetails)]
    public async Task<IActionResult> GetProductCategoryDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductCategoryDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ایجاد دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(ApiEndpoints.ProductCategory.CreateProductCategory)]
    public async Task<IActionResult> CreateProductCategory([FromForm] CreateProductCategoryDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCategoryCommand(createRequest));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ویرایش دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPut(ApiEndpoints.ProductCategory.EditProductCategory)]
    public async Task<IActionResult> EditProductCategory([FromForm] EditProductCategoryDto editRequest)
    {
        var res = await Mediator.Send(new EditProductCategoryCommand(editRequest));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    حذف دسته بندی محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete(ApiEndpoints.ProductCategory.DeleteProductCategory)]
    public async Task<IActionResult> DeleteProductCategory([FromRoute] long id)
    {
        var res = await Mediator.Send(new DeleteProductCategoryCommand(id));

        return JsonApiResult.Success(res);
    }
}