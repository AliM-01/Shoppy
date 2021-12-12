namespace Shoppy.WebApi.Endpoints.Admin;

public static class AdminInventoryApiEndpoints
{
    private const string BaseAdminInventoryRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/inventory";

    public static class Inventory
    {
        private const string BaseInventoryRoute = BaseAdminInventoryRoute;

        public const string FilterInventories = BaseInventoryRoute + "/filter";

    }


}
