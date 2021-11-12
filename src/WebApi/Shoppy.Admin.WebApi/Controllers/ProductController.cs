using Microsoft.AspNetCore.Mvc;
using Shoppy.Admin.WebApi.Endpoints;
using SM.Application.Contracts.Product.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace Shoppy.Admin.WebApi.Controllers;

public class ProductController : BaseApiController
{
    /// <summary>
    ///    فیلتر محصولات
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet(ApiEndpoints.Product.FilterProducts)]
    public async Task<IActionResult> FilterProductCategories([FromQuery] FilterProductDto filter)
    {
        var res = await Mediator.Send(new FilterProductsQuery(filter));

        return JsonApiResult.Success(res);
    }
}
