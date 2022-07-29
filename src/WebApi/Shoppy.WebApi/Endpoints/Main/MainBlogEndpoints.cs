namespace Shoppy.WebApi.Endpoints.Main;

public class MainBlogEndpoints
{
    private const string Base = BaseApiEndpointRoutes.BaseRoute + "/blog";

    public static class Article
    {
        private const string BaseArticle = Base + "/article";

        public const string Search = BaseArticle + "/search";

        public const string GetArticleDetails = BaseArticle + "/{slug}";

        public const string GetLatestArticles = BaseArticle + "/get-latest";

        public const string GetRelatedArticles = BaseArticle + "/get-related/{categoryId}";
    }

    public static class ArticleCategory
    {
        private const string BaseArticleCategory = Base + "/article-category";

        public const string GetArticleCategoryList = BaseArticleCategory + "/get-all";

        public const string GetArticleCategory = BaseArticleCategory + "/get";
    }
}