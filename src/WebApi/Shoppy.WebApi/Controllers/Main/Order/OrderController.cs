using AM.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using OM.Application.Contracts.Order.Commands;
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
        var res = await Mediator.Send(new ComputeCartQuery(items));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Checkout

    [HttpPost(MainOrderApiEndpoints.Order.Checkout)]
    [SwaggerOperation(Summary = "پردازش سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> Checkout([FromBody] List<CartItemInCookieDto> items)
    {
        var res = await Mediator.Send(new CheckoutCartQuery(items));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region PlaceOrder

    [HttpPost(MainOrderApiEndpoints.Order.PlaceOrder)]
    [Authorize(Policy = RoleConstants.BasicUser)]
    [SwaggerOperation(Summary = "ثبت سفارش", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> PlaceOrder([FromBody] CartDto cart)
    {
        var res = await Mediator.Send(new PlaceOrderCommand(cart, User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region InitializePayment

    [HttpPost(MainOrderApiEndpoints.Order.InitializePayment)]
    [Authorize(Policy = RoleConstants.BasicUser)]
    [SwaggerOperation(Summary = "ثبت پرداخت", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> InitializePayment([FromQuery] string oId,
            [FromQuery] decimal amount, [FromQuery] string callBack)
    {
        var payment = new InitializePaymentRequestDto
        {
            OrderId = oId,
            Amount = amount,
            CallBackUrl = callBack,
            Email = User.GetUserEmail()
        };

        var res = await Mediator.Send(new InitializePaymentRequestCommand(payment));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region VerifyPayment

    [HttpPost(MainOrderApiEndpoints.Order.VerifyPayment)]
    [Authorize(Policy = RoleConstants.BasicUser)]
    [SwaggerOperation(Summary = "تایید پرداخت", Tags = new[] { "Order" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> VerifyPayment([FromQuery] string authority, [FromQuery] string oId)
    {
        var res = await Mediator.Send(new VerifyPaymentRequestCommand(new VerifyPaymentRequestDto
        {
            OrderId = oId,
            Authority = authority,
        }, User.GetUserId()));

        return JsonApiResult.Success(res);
    }

    #endregion
}
