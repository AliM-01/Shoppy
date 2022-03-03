namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminBlogApiEndpoints
{
    private const string BaseAdminBlogRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/blog";

    public static class ArticleCategory
    {
        private const string BaseArticleCategoryRoute = BaseAdminBlogRoute + "/article-category";

        public const string GetArticleCategoriesSelectList = BaseArticleCategoryRoute + "/get-select-list";

        public const string FilterArticleCategories = BaseArticleCategoryRoute + "/filter";

        public const string GetArticleCategoryDetails = BaseArticleCategoryRoute + "/{id}";

        public const string CreateArticleCategory = BaseArticleCategoryRoute + "/create";

        public const string EditArticleCategory = BaseArticleCategoryRoute + "/edit";

        public const string DeleteArticleCategory = BaseArticleCategoryRoute + "/delete/{id}";
    }

    public static class Article
    {
        private const string BaseArticleRoute = BaseAdminBlogRoute + "/article";

        public const string FilterArticles = BaseArticleRoute + "/filter";

        public const string GetArticleDetails = BaseArticleRoute + "/{id}";

        public const string CreateArticle = BaseArticleRoute + "/create";

        public const string EditArticle = BaseArticleRoute + "/edit";

        public const string DeleteArticle = BaseArticleRoute + "/delete/{id}";
    }
}