#nullable enable

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
    [ProducesResponseType(typeof(CartDto), 200)]
    public async Task<IActionResult> ComputeCart([FromBody] List<CartItemInCookieDto> items)
    {
        var res = await Mediator.Send(new ComputeCartQuery(items));

        return SuccessResult(res);
    }

    #endregion

    #region Checkout

    [HttpPost(MainOrderEndpoints.Cart.Checkout)]
    [SwaggerOperation(Summary = "پردازش سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(CartDto), 200)]
    public async Task<IActionResult> Checkout([FromBody] List<CartItemInCookieDto> items, [FromQuery] string? discountCodeId)
    {
        var res = await Mediator.Send(new CheckoutCartQuery(items, discountCodeId));

        return SuccessResult(res);
    }

    #endregion
}
