namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminOrderEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/order";

    public static class Order
    {
        public const string FilterOrders = Base + "/filter";

        public const string GetUserOrders = Base + "/{userId}";

        public const string GetItems = Base + "/{orderId}/items/";

        public const string CancelOrder = Base + "/cancel/{orderId}";
    }
}