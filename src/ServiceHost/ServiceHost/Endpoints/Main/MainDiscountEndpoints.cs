namespace ServiceHost.Endpoints.Main;

public class MainDiscountEndpoints
{
    private const string Base = BaseApiEndpointRoutes.BaseRoute + "/discount";

    public static class DiscountCode
    {
        private const string BaseDiscountCode = Base + "/discount-code";

        public const string ValidateCode = BaseDiscountCode + "/validate/{code}";
    }
}