namespace Shoppy.WebApi.Endpoints.Main;

public class MainAccountApiEndpoints
{
    private const string BaseMainAccountRoute = BaseApiEndpointRoutes.BaseRoute + "/account";

    public static class Account
    {
        public const string Register = BaseMainAccountRoute + "/register";

        public const string Login = BaseMainAccountRoute + "/login";
    }
}
