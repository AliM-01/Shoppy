using AM.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using OM.Application.Contracts.Order.Commands;
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

    #region PlaceOrder

    [Authorize(Policy = RoleConstants.BasicUser)]
    [HttpPost(MainOrderEndpoints.Order.PlaceOrder)]
    [SwaggerOperation(Summary = "ثبت سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(Response<PlaceOrderResponseDto>), 200)]
    public async Task<IActionResult> PlaceOrder([FromBody] CartDto cart)
    {
        var res = await Mediator.Send(new PlaceOrderCommand(cart,
                                                            User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region CancelOrder

    [Authorize(Policy = RoleConstants.BasicUser)]
    [HttpDelete(MainOrderEndpoints.Order.CancelOrder)]
    [SwaggerOperation(Summary = "لفو سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<string>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> CancelOrder([FromRoute] string orderId)
    {
        var res = await Mediator.Send(new CancelOrderCommand(orderId,
                                                             User.GetUserId(),
                                                             false));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region InitializePayment

    [Authorize(Policy = RoleConstants.BasicUser)]
    [HttpPost(MainOrderEndpoints.Payment.InitializePayment)]
    [SwaggerOperation(Summary = "ثبت پرداخت", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(Response<InitializePaymentResponseDto>), 200)]
    public async Task<IActionResult> InitializePayment([FromQuery] string oId,
            [FromQuery] decimal amount, [FromQuery] string callBack)
    {
        var payment = new InitializePaymentRequestDto(oId,
                                                      amount,
                                                      callBack,
                                                      User.GetUserEmail());

        var res = await Mediator.Send(new InitializePaymentRequestCommand(payment));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region VerifyPayment

    [Authorize(Policy = RoleConstants.BasicUser)]
    [HttpPost(MainOrderEndpoints.Payment.VerifyPayment)]
    [SwaggerOperation(Summary = "تایید پرداخت", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(Response<VerifyPaymentResponseDto>), 200)]
    [ProducesResponseType(typeof(Response<string>), 404)]
    public async Task<IActionResult> VerifyPayment([FromQuery] string authority, [FromQuery] string oId)
    {
        var res = await Mediator.Send(new VerifyPaymentRequestCommand(new VerifyPaymentRequestDto(oId, authority),
                                                                      User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion
}
