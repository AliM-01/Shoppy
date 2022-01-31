namespace Shoppy.WebApi.Endpoints.Main;

public class MainCommentApiEndpoints
{
    private const string BaseMainCommentRoute = BaseApiEndpointRoutes.BaseRoute + "/comment";

    public static class Comment
    {
        public const string GetRecordCommentsById = BaseMainCommentRoute + "/get-comments/{recordId}";

        public const string AddComment = BaseMainCommentRoute + "/add";
    }
}
