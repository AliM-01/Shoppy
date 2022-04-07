namespace Shoppy.WebApi.Endpoints.User;

public class UserOrderEndpoints
{
    private const string Base = BaseApiEndpointRoutes.UserBaseRoute + "/order";

    public static class Order
    {
        public const string GetOrders = Base + "/all";

        public const string PlaceOrder = Base + "/place";

        public const string CancelOrder = Base + "/cancel/{orderId}";
    }

    public static class Payment
    {
        private const string BasePayment = Base + "/payment";

        public const string InitializePayment = BasePayment + "/init";

        public const string VerifyPayment = BasePayment + "/verify";
    }
}