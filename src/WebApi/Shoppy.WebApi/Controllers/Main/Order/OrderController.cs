using OM.Application.Contracts.Order.DTOs;
using OM.Application.Contracts.Order.Queries;
using System.Collections.Generic;

namespace Shoppy.WebApi.Controllers.Main.Order;

[SwaggerTag("سفارشات")]
public class OrderController : BaseApiController
{
    #region Compute Cart

    [HttpPost(MainOrderApiEndpoints.Order.ComputeCart)]
    [SwaggerOperation(Summary = "پردازش سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> ComputeCart([FromBody] List<CartItemInCookieDto> items)
    {
        var res = await Mediator.Send(new ComputeCartQuery(items, IsCheckout: false));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Compute Cart

    [HttpPost(MainOrderApiEndpoints.Order.Checkout)]
    [SwaggerOperation(Summary = "پردازش سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> Checkout([FromBody] List<CartItemInCookieDto> items)
    {
        var res = await Mediator.Send(new ComputeCartQuery(items, IsCheckout: true));

        return JsonApiResult.Success(res);
    }

    #endregion
}
