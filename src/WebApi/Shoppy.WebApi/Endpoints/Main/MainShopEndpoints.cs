namespace Shoppy.WebApi.Endpoints.Main;

public static class MainShopEndpoints
{
    private const string Base = BaseApiEndpointRoutes.BaseRoute + "/shop";

    public static class Slider
    {
        private const string BaseSlider = Base + "/slider";

        public const string GetSliders = BaseSlider + "/get-all";
    }

    public static class ProductCategory
    {
        private const string BaseProductCategory = Base + "/product-category";

        public const string GetProductCategoryList = BaseProductCategory + "/get-all";

        public const string GetProductCategory = BaseProductCategory + "/get";
    }

    public static class Product
    {
        private const string BaseProduct = Base + "/product";

        public const string GetProductDetails = BaseProduct + "/{slug}";

        public const string Search = BaseProduct + "/search";

        public const string GetLatestProducts = BaseProduct + "/latest";

        public const string GetRelatedProducts = BaseProduct + "/get-related/{categoryId}";

        public const string GetHotestDiscountProducts = BaseProduct + "/hotest-discount";
    }
}