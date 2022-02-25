namespace Shoppy.WebApi.Endpoints.Main;

public class MainSiteApiEndpoints
{
    private const string BaseMainSiteRoute = BaseApiEndpointRoutes.BaseRoute + "/site";

    public static class Site
    {
        public const string GetMenu = BaseMainSiteRoute + "/get-menu";
    }
}
