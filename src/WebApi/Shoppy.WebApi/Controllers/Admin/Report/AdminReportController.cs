using _03_Reports.Query.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("گزارشات")]
public class AdminReportController : BaseAdminApiController
{
    #region Filter Order

    [HttpGet(AdminReportApiEndpoints.Orders)]
    [SwaggerOperation(Summary = "گزارش سفارشات", Tags = new[] { "AdminOrder" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> OrdersReport()
    {
        var res = await Mediator.Send(new GetOrdersChartQuery());

        return JsonApiResult.Success(res);
    }

    #endregion
}
