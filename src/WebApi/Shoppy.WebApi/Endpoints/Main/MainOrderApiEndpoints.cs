namespace Shoppy.WebApi.Endpoints.Main;

public class MainOrderApiEndpoints
{
    private const string BaseMainOrderRoute = BaseApiEndpointRoutes.BaseRoute + "/order";

    public static class Order
    {
        public const string ComputeCart = BaseMainOrderRoute + "/cart/compute";

        public const string Checkout = BaseMainOrderRoute + "/cart/checkout";

        public const string PlaceOrder = BaseMainOrderRoute + "/place";

        public const string CancelOrder = BaseMainOrderRoute + "/cancel/{orderId}";

        public const string InitializePayment = BaseMainOrderRoute + "/payment/initialize";

        public const string VerifyPayment = BaseMainOrderRoute + "/payment/verify";
    }
}
