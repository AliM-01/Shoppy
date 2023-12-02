namespace ServiceHost.Endpoints.Main;

public class MainAccountEndpoints
{
    private const string Base = BaseApiEndpointRoutes.BaseRoute + "/account";

    public static class Account
    {
        public const string Register = Base + "/register";

        public const string Login = Base + "/login";

        public const string ExternalLogin = Base + "/external-login";

        public const string GetExternalLogins = Base + "/external-logins-schema";

        public const string ExternalLoginCallBack = Base + "/external-login-callback";

        public const string ConfirmEmail = Base + "/confirm-email";

        public const string RefreshToken = Base + "/refresh-token";

        public const string ForgotPassword = Base + "/forgot-password";

        public const string Logout = Base + "/logout";

        public const string IsAuthenticated = Base + "/is-authenticated";

        public const string IsInRole = Base + "/is-in-role";

        public const string GetCurrentUser = Base + "/get-currentUser";
    }
}