namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminAccountApiEndpoints
{
    private const string BaseAdminAccountRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/account";

    public static class Account
    {
        public const string FilterAccounts = BaseAdminAccountRoute + "/filter";

        public const string GetAccountDetails = BaseAdminAccountRoute + "/{id}";

        public const string EditAccount = BaseAdminAccountRoute + "/edit";

        public const string ActivateAccount = BaseAdminAccountRoute + "/activate/{id}";

        public const string DeActivateAccount = BaseAdminAccountRoute + "/de-activate/{id}";
    }
}