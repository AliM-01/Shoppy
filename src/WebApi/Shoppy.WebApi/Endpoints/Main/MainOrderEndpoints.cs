namespace Shoppy.WebApi.Endpoints.Main;

public class MainOrderEndpoints
{
    private const string Base = BaseApiEndpointRoutes.BaseRoute + "/order";

    public static class Cart
    {
        private const string BaseCart = Base + "/cart";

        public const string ComputeCart = BaseCart + "/compute";

        public const string Checkout = BaseCart + "/checkout";
    }
}
