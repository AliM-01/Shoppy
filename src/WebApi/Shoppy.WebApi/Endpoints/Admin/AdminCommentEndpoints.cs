namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminCommentEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/comment";

    public static class Comment
    {
        public const string FilterComments = Base + "/filter";

        public const string ConfirmComment = Base + "/cofirm/{id}";

        public const string CancelComment = Base + "/cancel/{id}";

    }
}