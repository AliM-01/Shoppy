namespace Shoppy.WebApi.Endpoints.Main;

public class MainOrderEndpoints
{
    private const string Base = BaseApiEndpointRoutes.BaseRoute + "/order";

    public static class Order
    {
        public const string PlaceOrder = Base + "/place";

        public const string CancelOrder = Base + "/cancel/{orderId}";
    }

    public static class Cart
    {
        private const string BaseCart = Base + "/cart";

        public const string ComputeCart = BaseCart + "/compute";

        public const string Checkout = BaseCart + "/checkout";
    }

    public static class Payment
    {
        private const string BasePayment = Base + "/payment";

        public const string InitializePayment = BasePayment + "/init";

        public const string VerifyPayment = BasePayment + "/verify";
    }
}
