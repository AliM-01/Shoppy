namespace Shoppy.WebApi.Endpoints.Main;

public class MainOrderApiEndpoints
{
    private const string BaseMainOrderRoute = BaseApiEndpointRoutes.BaseRoute + "/order";

    public static class Order
    {
        public const string ComputeCart = BaseMainOrderRoute + "/compute-cart";
    }
}
