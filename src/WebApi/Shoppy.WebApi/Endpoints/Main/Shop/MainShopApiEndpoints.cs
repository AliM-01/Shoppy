namespace Shoppy.WebApi.Endpoints.Main.Shop;

public static class MainShopApiEndpoints
{
    public static class Slider
    {
        private const string BaseSliderRoute = BaseApiEndpointRoutes.BaseRoute + "/slider";

        public const string GetSliders = BaseSliderRoute + "/get-list";
    }

    public static class ProductCategory
    {
        private const string BaseProductCategoryRoute = BaseApiEndpointRoutes.BaseRoute + "/product-category";

        public const string GetProductCategories = BaseProductCategoryRoute + "/get-list";
    }
}
