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

}
