namespace Shoppy.WebApi.Endpoints.Main;

public class MainCommentEndpoints
{
    private const string Base = BaseApiEndpointRoutes.BaseRoute + "/comment";

    public static class Comment
    {
        public const string GetRecordCommentsById = Base + "/get-comments/{recordId}";

        public const string AddComment = Base + "/add";
    }
}
