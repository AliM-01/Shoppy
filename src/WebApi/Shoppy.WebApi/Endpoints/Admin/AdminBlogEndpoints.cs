namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminBlogEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/blog";

    public static class ArticleCategory
    {
        private const string BaseArticleCategory = Base + "/article-category";

        public const string GetArticleCategoriesSelectList = BaseArticleCategory + "/get-select-list";

        public const string FilterArticleCategories = BaseArticleCategory + "/filter";

        public const string GetArticleCategoryDetails = BaseArticleCategory + "/{id}";

        public const string CreateArticleCategory = BaseArticleCategory;

        public const string EditArticleCategory = BaseArticleCategory;

        public const string DeleteArticleCategory = BaseArticleCategory + "/{id}";
    }

    public static class Article
    {
        private const string BaseArticle = Base + "/article";

        public const string FilterArticles = BaseArticle + "/filter";

        public const string GetArticleDetails = BaseArticle + "/{id}";

        public const string CreateArticle = BaseArticle;

        public const string EditArticle = BaseArticle;

        public const string DeleteArticle = BaseArticle + "/{id}";
    }
}