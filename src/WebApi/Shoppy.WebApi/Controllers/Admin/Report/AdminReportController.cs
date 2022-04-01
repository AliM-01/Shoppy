using _03_Reports.Query.Queries;

namespace Shoppy.WebApi.Controllers.Admin.Discount;

[SwaggerTag("گزارشات")]
public class AdminReportController : BaseAdminApiController
{
    #region Order

    [HttpGet(AdminReportEndpoints.Orders)]
    [SwaggerOperation(Summary = "گزارش سفارشات", Tags = new[] { "AdminReport" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> OrdersReport()
    {
        var res = await Mediator.Send(new GetOrdersChartQuery());

        return JsonApiResult.Success(res);
    }

    #endregion

    #region ProductSales

    [HttpGet(AdminReportEndpoints.ProductSales)]
    [SwaggerOperation(Summary = "گزارش محصولات فروخته شده", Tags = new[] { "AdminReport" })]
    [SwaggerResponse(200, "success")]
    public async Task<IActionResult> ProductSales()
    {
        var res = await Mediator.Send(new GetProductsSoldChartQuery());

        return JsonApiResult.Success(res);
    }

    #endregion
}
