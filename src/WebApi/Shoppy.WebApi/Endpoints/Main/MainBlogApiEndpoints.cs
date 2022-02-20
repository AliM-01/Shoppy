namespace Shoppy.WebApi.Endpoints.Main;

public class MainBlogApiEndpoints
{
    private const string BaseMainBlogRoute = BaseApiEndpointRoutes.BaseRoute + "/blog";

    public static class Article
    {
        private const string BaseArticleRoute = BaseMainBlogRoute + "/article";

        public const string GetArticleDetails = BaseArticleRoute + "/{slug}";

        public const string GetLatestArticles = BaseArticleRoute + "/get-latest";
    }

    public static class ArticleCategory
    {
        private const string BaseArticleCategoryRoute = BaseMainBlogRoute + "/article-category";

        public const string GetArticleCategory = BaseArticleCategoryRoute + "/get";
    }
}
