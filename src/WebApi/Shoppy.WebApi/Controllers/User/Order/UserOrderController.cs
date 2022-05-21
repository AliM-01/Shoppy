using Microsoft.AspNetCore.WebUtilities;
using OM.Application.Contracts.Order.Commands;
using OM.Application.Contracts.Order.DTOs;
using OM.Application.Contracts.Order.Queries;
using System.Text;

namespace Shoppy.WebApi.Controllers.User.Order;

[SwaggerTag("سفارشات کاربر")]
public class UserOrderController : BaseUserApiController
{
    #region Get User Orders

    [HttpGet(UserOrderEndpoints.Order.GetMyOrders)]
    [SwaggerOperation(Summary = "دریافت سفارش های من", Tags = new[] { "UserOrder" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(204, "no-content")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(FilterUserOrdersDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 204)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetMyOrders([FromQuery] FilterUserOrdersDto filter)
    {
        var res = await Mediator.Send(new GetUserOrdersQuery(User.GetUserId(), filter));

        return SuccessResult(res);
    }

    #endregion

    #region PlaceOrder

    [HttpPost(UserOrderEndpoints.Order.PlaceOrder)]
    [SwaggerOperation(Summary = "ثبت سفارش", Tags = new[] { "UserOrder" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(PlaceOrderResponseDto), 200)]
    public async Task<IActionResult> PlaceOrder([FromBody] CartDto cart)
    {
        var res = await Mediator.Send(new PlaceOrderCommand(cart,
                                                            User.GetUserId()));

        return SuccessResult(res);
    }

    #endregion

    #region CancelOrder

    [HttpDelete(UserOrderEndpoints.Order.CancelOrder)]
    [SwaggerOperation(Summary = "لفو سفارش", Tags = new[] { "UserOrder" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> CancelOrder([FromRoute] string orderId)
    {
        var res = await Mediator.Send(new CancelOrderCommand(orderId,
                                                             User.GetUserId(),
                                                             false));

        return SuccessResult(res);
    }

    #endregion

    #region InitializePayment

    [HttpPost(UserOrderEndpoints.Payment.InitializePayment)]
    [SwaggerOperation(Summary = "ثبت پرداخت", Tags = new[] { "UserOrder" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(InitializePaymentResponseDto), 200)]
    public async Task<IActionResult> InitializePayment([FromQuery] string oId, [FromQuery] decimal amount, [FromQuery] string callBack)
    {
        oId = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(oId));

        var payment = new InitializePaymentRequestDto(oId,
                                                      amount,
                                                      callBack,
                                                      User.GetUserEmail());

        var res = await Mediator.Send(new InitializePaymentRequestCommand(payment));

        return SuccessResult(res);
    }

    #endregion

    #region VerifyPayment

    [HttpPost(UserOrderEndpoints.Payment.VerifyPayment)]
    [SwaggerOperation(Summary = "تایید پرداخت", Tags = new[] { "UserOrder" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(VerifyPaymentResponseDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> VerifyPayment([FromQuery] string authority, [FromQuery] string oId)
    {
        oId = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(oId));

        var res = await Mediator.Send(new VerifyPaymentRequestCommand(new VerifyPaymentRequestDto(authority, oId),
                                                                      User.GetUserId()));

        return SuccessResult(res);
    }

    #endregion

}
