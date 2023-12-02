namespace ServiceHost.Endpoints.Admin;

public static class AdminShopEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/shop";

    public static class ProductCategory
    {
        private const string BaseProductCategory = Base + "/product-category";

        public const string GetProductCategoriesList = BaseProductCategory + "/get-list";

        public const string FilterProductCategories = BaseProductCategory + "/filter";

        public const string GetProductCategoryDetails = BaseProductCategory + "/{id}";

        public const string CreateProductCategory = BaseProductCategory;

        public const string EditProductCategory = BaseProductCategory;

        public const string DeleteProductCategory = BaseProductCategory + "/{id}";
    }

    public static class Product
    {
        private const string BaseProduct = Base + "/product";

        public const string FilterProducts = BaseProduct + "/filter";

        public const string GetProductDetails = BaseProduct + "/{id}";

        public const string ExistsProductId = BaseProduct + "/exists/{id}";

        public const string CreateProduct = BaseProduct;

        public const string EditProduct = BaseProduct;

        public const string DeleteProduct = BaseProduct + "/{id}";
    }

    public static class ProductPicture
    {
        private const string BaseProductPicture = Base + "/product-picture";

        public const string GetProductPictures = BaseProductPicture + "/{productId}";

        public const string CreateProductPicture = BaseProductPicture + "/create/{productId}";

        public const string RemoveProductPicture = BaseProductPicture + "/remove/{id}";
    }

    public static class ProductFeature
    {
        private const string BaseProductFeature = Base + "/product-feature";

        public const string FilterProductFeatures = BaseProductFeature + "/filter";

        public const string GetProductFeatureDetails = BaseProductFeature + "/{id}";

        public const string CreateProductFeature = BaseProductFeature + "/create";

        public const string EditProductFeature = BaseProductFeature + "/edit";

        public const string DeleteProductFeature = BaseProductFeature + "/delete/{id}";
    }

    public static class Slider
    {
        private const string BaseSlider = Base + "/slider";

        public const string GetSlidersList = BaseSlider + "/get-list";

        public const string GetSliderDetails = BaseSlider + "/{id}";

        public const string CreateSlider = BaseSlider + "/create";

        public const string EditSlider = BaseSlider + "/edit";

        public const string RemoveSlider = BaseSlider + "/remove/{id}";

        public const string RestoreSlider = BaseSlider + "/restore/{id}";
    }
}