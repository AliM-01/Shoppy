namespace Shoppy.WebApi.Endpoints.Admin.Shop;

public static class AdminShopApiEndpoints
{
    public static class ProductCategory
    {
        private const string BaseProductCategoryRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/product-category";

        public const string GetProductCategoriesList = BaseProductCategoryRoute + "/get-list";

        public const string FilterProductCategories = BaseProductCategoryRoute + "/filter-product-categories";

        public const string GetProductCategoryDetails = BaseProductCategoryRoute + "/{id}";

        public const string CreateProductCategory = BaseProductCategoryRoute + "/create-product-category";

        public const string EditProductCategory = BaseProductCategoryRoute + "/edit-product-category";

        public const string DeleteProductCategory = BaseProductCategoryRoute + "/delete-product-category/{id}";
    }

    public static class Product
    {
        private const string BaseProductRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/product";

        public const string FilterProducts = BaseProductRoute + "/filter-products";

        public const string GetProductDetails = BaseProductRoute + "/{id}";

        public const string CreateProduct = BaseProductRoute + "/create-product";

        public const string EditProduct = BaseProductRoute + "/edit-product";

        public const string DeleteProduct = BaseProductRoute + "/delete-product/{id}";

        public const string UpdateProductIsInStock = BaseProductRoute + "/update-product-is-in-stock/{id}";

        public const string UpdateProductNotInStock = BaseProductRoute + "/update-product-not-in-stock/{id}";
    }

    public static class ProductPicture
    {
        private const string BaseProductPictureRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/product-picture";

        public const string GetProductPictures = BaseProductPictureRoute + "/{productId}";

        public const string CreateProductPicture = BaseProductPictureRoute + "/create-product-picture";

        public const string RemoveProductPicture = BaseProductPictureRoute + "/remove-product-picture/{id}";

    }

    public static class Slider
    {
        private const string BaseSliderRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/slider";

        public const string GetSlidersList = BaseSliderRoute + "/get-list";

        public const string GetSliderDetails = BaseSliderRoute + "/{id}";

        public const string CreateSlider = BaseSliderRoute + "/create-slider";

        public const string EditSlider = BaseSliderRoute + "/edit-slider";

        public const string RemoveSlider = BaseSliderRoute + "/remove-slider/{id}";

        public const string RestoreSlider = BaseSliderRoute + "/restore-slider/{id}";
    }
}
