namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminReportEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/report";

    public const string Orders = Base + "/orders";

    public const string ProductSales = Base + "/product-sales";
}