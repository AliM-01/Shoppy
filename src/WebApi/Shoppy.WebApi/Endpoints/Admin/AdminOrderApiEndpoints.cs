namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminOrderApiEndpoints
{
    private const string BaseAdminOrderRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/order";

    public static class Order
    {
        private const string BaseOrderRoute = BaseAdminOrderRoute + "/order";

        public const string FilterOrders = BaseOrderRoute + "/filter";
    }
}