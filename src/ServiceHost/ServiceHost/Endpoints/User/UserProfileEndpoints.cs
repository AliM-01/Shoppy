namespace ServiceHost.Endpoints.User;

public class UserProfileEndpoints
{
    private const string Base = BaseApiEndpointRoutes.UserBaseRoute + "/profile";

    public static class Profile
    {
        public const string GetProfile = Base;
    }
}