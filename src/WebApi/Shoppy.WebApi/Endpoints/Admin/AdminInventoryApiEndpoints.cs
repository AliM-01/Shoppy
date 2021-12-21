namespace Shoppy.WebApi.Endpoints.Admin;

public static class AdminInventoryApiEndpoints
{
    private const string BaseAdminInventoryRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/inventory";

    public static class Inventory
    {
        private const string BaseInventoryRoute = BaseAdminInventoryRoute;

        public const string FilterInventories = BaseInventoryRoute + "/filter";

        public const string GetInventoryDetails = BaseInventoryRoute + "/{id}";

        public const string CreateInventory = BaseInventoryRoute + "/create";

        public const string EditInventory = BaseInventoryRoute + "/edit";

        public const string IncreaseInventory = BaseInventoryRoute + "/increase";

        public const string ReduceInventory = BaseInventoryRoute + "/reduce";

        public const string GetInventoryOperationLog = BaseInventoryRoute + "/get-operation-log/{id}";
    }


}
