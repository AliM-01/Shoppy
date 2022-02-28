namespace Shoppy.WebApi.Endpoints.Main;

public class MainAccountApiEndpoints
{
    private const string BaseMainAccountRoute = BaseApiEndpointRoutes.BaseRoute + "/account";

    public static class Account
    {
        public const string Register = BaseMainAccountRoute + "/register";

        public const string Login = BaseMainAccountRoute + "/login";

        public const string RefreshToken = BaseMainAccountRoute + "/refresh-token";

        public const string ForgotPassword = BaseMainAccountRoute + "/forgot-password";

        public const string Logout = BaseMainAccountRoute + "/logout";

        public const string IsAuthenticated = BaseMainAccountRoute + "/is-authenticated";

        public const string GetCurrentUser = BaseMainAccountRoute + "/get-currentUser";
    }
}
