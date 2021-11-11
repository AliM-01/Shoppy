namespace Shoppy.Admin.WebApi.Endpoints;
public static class ApiEndpoints
{
    public const string BaseRoute = "api";

    public static class ProductCategory
    {
        private const string BaseProductCategoryRoute = BaseRoute + "/product-category";

        public const string FilterProductCategories = BaseProductCategoryRoute + "/filter-product-categories";

        public const string GetProductCategoryDetails = BaseProductCategoryRoute + "/{id}";

        public const string CreateProductCategory = BaseProductCategoryRoute + "/create-product-category";

        public const string EditProductCategory = BaseProductCategoryRoute + "/edit-product-category/{id}";

        public const string DeleteProductCategory = BaseProductCategoryRoute + "/delete-product-category/{id}";
    }
}