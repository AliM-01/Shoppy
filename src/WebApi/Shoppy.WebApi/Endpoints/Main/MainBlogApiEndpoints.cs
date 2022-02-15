namespace Shoppy.WebApi.Endpoints.Main;

public class MainBlogApiEndpoints
{
    private const string BaseMainBlogRoute = BaseApiEndpointRoutes.BaseRoute + "/blog";

    public static class Article
    {
        private const string BaseArticleRoute = BaseMainBlogRoute + "/article";

        public const string GetLatestArticles = BaseArticleRoute + "/get-latest";
    }
}
