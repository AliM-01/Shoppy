using _0_Framework.Application.Models.Reports;
using OM.Application.Reports;
using SM.Application.Reports;

namespace ServiceHost.Controllers.Admin.Discount;

[SwaggerTag("گزارشات")]
public class AdminReportController : BaseAdminApiController
{
    #region Order

    [HttpGet(AdminReportEndpoints.Orders)]
    [SwaggerOperation(Summary = "گزارش سفارشات", Tags = new[] { "AdminReport" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<ChartModel>), 200)]
    public async Task<IActionResult> OrdersReport()
    {
        var res = await Mediator.Send(new GetOrdersChartQuery());

        return SuccessResult(res);
    }

    #endregion

    #region ProductSales

    [HttpGet(AdminReportEndpoints.ProductSales)]
    [SwaggerOperation(Summary = "گزارش محصولات فروخته شده", Tags = new[] { "AdminReport" })]
    [SwaggerResponse(200, "success")]
    [ProducesResponseType(typeof(IEnumerable<ChartModel>), 200)]
    public async Task<IActionResult> ProductSales()
    {
        var res = await Mediator.Send(new GetSoldProductsChartQuery());

        return SuccessResult(res);
    }

    #endregion
}