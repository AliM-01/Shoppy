using OM.Application.Contracts.Order.Commands;
using OM.Application.Contracts.Order.DTOs;
using OM.Application.Contracts.Order.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("مدیریت سفارشات")]
public class AdminOrderController : BaseAdminApiController
{
    #region Filter Order

    [HttpGet(AdminOrderApiEndpoints.Order.FilterOrders)]
    [SwaggerOperation(Summary = "فیلتر سفارشات", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> FilterInventories([FromQuery] FilterOrderDto filter)
    {
        var res = await Mediator.Send(new FilterOrdersQuery(filter));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Get Items

    [HttpGet(AdminOrderApiEndpoints.Order.GetItems)]
    [SwaggerOperation(Summary = "دریافت آیتم های سفارش", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> GetItems([FromRoute] string orderId)
    {
        var res = await Mediator.Send(new GetInventoryItemsQuery(orderId, User.GetUserId(), true));

        return JsonApiResult.Success(res);
    }

    #endregion

    #region Cancel Order

    [HttpDelete(AdminOrderApiEndpoints.Order.CancelOrder)]
    [SwaggerOperation(Summary = "لغو سفارش", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> CancelOrder([FromRoute] string orderId)
    {
        var res = await Mediator.Send(new CancelOrderCommand(orderId, User.GetUserId(), true));

        return JsonApiResult.Success(res);
    }

    #endregion
}
