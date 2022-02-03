namespace Shoppy.WebApi.Endpoints.Admin;

public class AdminBlogBlogApiEndpoints
{
    private const string BaseAdminBlogRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/blog";

    public static class ArticleCategory
    {
        private const string BaseArticleCategoryRoute = BaseAdminBlogRoute + "/article-category";

        public const string FilterArticleCategories = BaseArticleCategoryRoute + "/filter";

        public const string GetArticleCategoryDetails = BaseArticleCategoryRoute + "/{id}";

        public const string CreateArticleCategory = BaseArticleCategoryRoute + "/create";

        public const string EditArticleCategory = BaseArticleCategoryRoute + "/edit";

        public const string DeleteArticleCategory = BaseArticleCategoryRoute + "/delete/{id}";
    }

}