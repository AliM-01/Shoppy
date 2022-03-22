namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminOrderApiEndpoints
{
    private const string BaseAdminOrderRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/order";

    public static class Order
    {
        public const string FilterOrders = BaseAdminOrderRoute + "/filter";

        public const string GetItems = BaseAdminOrderRoute + "/items/{orderId}";

        public const string CancelOrder = BaseAdminOrderRoute + "/cancel/{orderId}";
    }
}