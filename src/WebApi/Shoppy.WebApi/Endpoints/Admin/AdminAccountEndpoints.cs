namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminAccountEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/account";

    public static class Account
    {
        public const string FilterAccounts = Base + "/filter";

        public const string GetAccountDetails = Base + "/{id}";

        public const string EditAccount = Base + "/edit";

        public const string ActivateAccount = Base + "/activate/{id}";

        public const string DeActivateAccount = Base + "/de-activate/{id}";
    }
}