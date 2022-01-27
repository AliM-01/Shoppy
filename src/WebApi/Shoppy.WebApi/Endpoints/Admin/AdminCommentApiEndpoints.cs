namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminCommentApiEndpoints
{
    private const string BaseAdminCommentRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/comment";

    public static class Comment
    {
        private const string BaseCommentRoute = BaseAdminCommentRoute;

        public const string FilterComments = BaseCommentRoute + "/filter";

        public const string ConfirmComment = BaseCommentRoute + "/cofirm/{id}";

        public const string CancelComment = BaseCommentRoute + "/cancel/{id}";

    }
}