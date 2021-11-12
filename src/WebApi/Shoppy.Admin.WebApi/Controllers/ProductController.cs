using Microsoft.AspNetCore.Mvc;
using Shoppy.Admin.WebApi.Endpoints;
using SM.Application.Contracts.Product.Commands;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.Product.Queries;

namespace Shoppy.Admin.WebApi.Controllers;

public class ProductController : BaseApiController
{
    /// <summary>
    ///    فیلتر محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.Product.FilterProducts)]
    public async Task<IActionResult> FilterProducts([FromQuery] FilterProductDto filter)
    {
        var res = await Mediator.Send(new FilterProductsQuery(filter));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    دریافت جزییات محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.Product.GetProductDetails)]
    public async Task<IActionResult> GetProductDetails([FromRoute] long id)
    {
        var res = await Mediator.Send(new GetProductDetailsQuery(id));

        return JsonApiResult.Success(res);
    }

    /// <summary>
    ///    ایجاد محصول
    /// </summary>
    /// <response code="200">Success</response>
    [HttpPost(ApiEndpoints.Product.CreateProduct)]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createRequest)
    {
        var res = await Mediator.Send(new CreateProductCommand(createRequest));

        return JsonApiResult.Success(res);
    }
}
