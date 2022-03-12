namespace Shoppy.WebApi.Endpoints.Main;

public class MainOrderApiEndpoints
{
    private const string BaseMainOrderRoute = BaseApiEndpointRoutes.BaseRoute + "/order";

    public static class Order
    {
        public const string ComputeCart = BaseMainOrderRoute + "/compute-cart";

        public const string Checkout = BaseMainOrderRoute + "/checkout";

        public const string PlaceOrder = BaseMainOrderRoute + "/place-order";

        public const string InitializePayment = BaseMainOrderRoute + "/initialize-payment";

        public const string VerifyPayment = BaseMainOrderRoute + "/verify-payment";
    }
}
