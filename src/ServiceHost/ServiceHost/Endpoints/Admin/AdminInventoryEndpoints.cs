namespace ServiceHost.Endpoints.Admin;

public static class AdminInventoryEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/inventory";

    public static class Inventory
    {
        public const string FilterInventories = Base + "/filter";

        public const string GetInventoryDetails = Base + "/{id}";

        public const string EditInventory = Base;

        public const string IncreaseInventory = Base + "/increase";

        public const string ReduceInventory = Base + "/reduce";

        public const string GetInventoryOperationLog = Base + "/{id}/logs";
    }
}