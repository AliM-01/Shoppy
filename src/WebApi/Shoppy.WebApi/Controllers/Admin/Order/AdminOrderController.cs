using OM.Application.Order.Commands;
using OM.Application.Order.DTOs;
using OM.Application.Order.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Order;

[SwaggerTag("مدیریت سفارشات")]
public class AdminOrderController : BaseAdminApiController
{
    #region Filter Order

    [HttpGet(AdminOrderEndpoints.Order.FilterOrders)]
    [SwaggerOperation(Summary = "فیلتر سفارشات", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(FilterOrderDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterOrderDto filter)
    {
        var res = await Mediator.Send(new FilterOrdersQuery(filter));

        return SuccessResult(res);
    }

    #endregion

    #region Get User Orders

    [HttpGet(AdminOrderEndpoints.Order.GetUserOrders)]
    [SwaggerOperation(Summary = "دریافت سفارش های کاربر", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(204, "no-content")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(FilterUserOrdersDto), 200)]
    [ProducesResponseType(typeof(ApiResult), 204)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetUserOrders([FromRoute] string userId, [FromQuery] FilterUserOrdersDto filter)
    {
        var res = await Mediator.Send(new GetUserOrdersQuery(userId, filter));

        return SuccessResult(res);
    }

    #endregion

    #region Get Items

    [HttpGet(AdminOrderEndpoints.Order.GetItems)]
    [SwaggerOperation(Summary = "دریافت آیتم های سفارش", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(IEnumerable<OrderItemDto>), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> GetItems([FromRoute] string orderId)
    {
        var res = await Mediator.Send(new GetOrderItemsQuery(orderId,
            User.GetUserId(),
            true));

        return SuccessResult(res);
    }

    #endregion

    #region Cancel Order

    [HttpDelete(AdminOrderEndpoints.Order.CancelOrder)]
    [SwaggerOperation(Summary = "لغو سفارش", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    [SwaggerResponse(404, "not-found")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    [ProducesResponseType(typeof(ApiResult), 404)]
    public async Task<IActionResult> CancelOrder([FromRoute] string orderId)
    {
        var res = await Mediator.Send(new CancelOrderCommand(orderId,
            User.GetUserId(),
            true));

        return SuccessResult(res);
    }

    #endregion
}