using OM.Application.Contracts.Order.DTOs;
using OM.Application.Contracts.Order.Queries;

namespace Shoppy.WebApi.Controllers.Main.Order;

[SwaggerTag("سفارشات")]
public class OrderController : BaseApiController
{
    #region Compute Cart

    [HttpPost(MainOrderEndpoints.Cart.ComputeCart)]
    [SwaggerOperation(Summary = "پردازش سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(Response<CartDto>), 200)]
    public async Task<IActionResult> ComputeCart([FromBody] List<CartItemInCookieDto> items)
    {
        var res = await Mediator.Send(new ComputeCartQuery(items));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Checkout

    [HttpPost(MainOrderEndpoints.Cart.Checkout)]
    [SwaggerOperation(Summary = "پردازش سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(Response<CartDto>), 200)]
    public async Task<IActionResult> Checkout([FromBody] List<CartItemInCookieDto> items)
    {
        var res = await Mediator.Send(new CheckoutCartQuery(items));

        return JsonApiResult.Success(res);
    }

    #endregion
}
