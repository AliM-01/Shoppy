namespace Shoppy.WebApi.Endpoints;
public static class ApiEndpoints
{
    #region Admin

    #region Shop

    public static class AdminProductCategory
    {
        private const string BaseProductCategoryRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/product-category";

        public const string GetProductCategoriesList = BaseProductCategoryRoute + "/get-list";

        public const string FilterProductCategories = BaseProductCategoryRoute + "/filter-product-categories";

        public const string GetProductCategoryDetails = BaseProductCategoryRoute + "/{id}";

        public const string CreateProductCategory = BaseProductCategoryRoute + "/create-product-category";

        public const string EditProductCategory = BaseProductCategoryRoute + "/edit-product-category";

        public const string DeleteProductCategory = BaseProductCategoryRoute + "/delete-product-category/{id}";
    }

    public static class AdminProduct
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

    public static class AdminProductPicture
    {
        private const string BaseProductPictureRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/product-picture";

        public const string GetProductPictures = BaseProductPictureRoute + "/{productId}";

        public const string CreateProductPicture = BaseProductPictureRoute + "/create-product-picture";

        public const string RemoveProductPicture = BaseProductPictureRoute + "/remove-product-picture/{id}";

    }

    public static class AdminSlider
    {
        private const string BaseSliderRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/slider";

        public const string GetSlidersList = BaseSliderRoute + "/get-list";

        public const string GetSliderDetails = BaseSliderRoute + "/{id}";

        public const string CreateSlider = BaseSliderRoute + "/create-slider";

        public const string EditSlider = BaseSliderRoute + "/edit-slider";

        public const string RemoveSlider = BaseSliderRoute + "/remove-slider/{id}";

        public const string RestoreSlider = BaseSliderRoute + "/restore-slider/{id}";
    }

    #endregion

    #endregion

    #region Main

    #region Shop

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

    #endregion

    #endregion
}