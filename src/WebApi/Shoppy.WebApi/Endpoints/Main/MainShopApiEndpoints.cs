namespace Shoppy.WebApi.Endpoints.Main;

public static class MainShopApiEndpoints
{
    private const string BaseMainShopRoute = BaseApiEndpointRoutes.BaseRoute + "/shop";

    public static class Slider
    {
        private const string BaseSliderRoute = BaseMainShopRoute + "/slider";

        public const string GetSliders = BaseSliderRoute + "/get-list";
    }

    public static class ProductCategory
    {
        private const string BaseProductCategoryRoute = BaseMainShopRoute + "/product-category";

        public const string GetProductCategories = BaseProductCategoryRoute + "/get-list";

        public const string GetProductCategory = BaseProductCategoryRoute + "/get";
    }

    public static class Product
    {
        private const string BaseProductRoute = BaseMainShopRoute + "/product";

        public const string Search = BaseProductRoute + "/search";

        public const string GetLatestProducts = BaseProductRoute + "/get-latest";

        public const string GetHotestDiscountProducts = BaseProductRoute + "/get-hotest-discount";
    }
}
