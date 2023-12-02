namespace ServiceHost.Endpoints.Admin;

public class AdminCommentEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/comment";

    public static class Comment
    {
        public const string FilterComments = Base + "/filter";

        public const string ConfirmComment = Base + "/confirm/{id}";

        public const string CancelComment = Base + "/cancel/{id}";
    }
}