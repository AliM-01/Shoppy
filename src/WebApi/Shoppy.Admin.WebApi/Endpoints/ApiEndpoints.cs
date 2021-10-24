namespace Shoppy.Admin.WebApi.Endpoints
{
    public static class ApiEndpoints
    {
        public const string BaseRoute = "api";

        public static class ProductCategory
        {
            private const string ProductCategoryRoute = BaseRoute + "/product-category";

            public const string GetSliders = ProductCategoryRoute + "/get-sliders";
        }
    }
}