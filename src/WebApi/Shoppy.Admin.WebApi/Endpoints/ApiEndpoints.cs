namespace Shoppy.Admin.WebApi.Endpoints;
public static class ApiEndpoints
{
    public const string BaseRoute = "api";

    public static class ProductCategory
    {
        private const string BaseProductCategoryRoute = BaseRoute + "/product-category";

        public const string GetProductCategoriesList = BaseProductCategoryRoute + "/get-list";

        public const string FilterProductCategories = BaseProductCategoryRoute + "/filter-product-categories";

        public const string GetProductCategoryDetails = BaseProductCategoryRoute + "/{id}";

        public const string CreateProductCategory = BaseProductCategoryRoute + "/create-product-category";

        public const string EditProductCategory = BaseProductCategoryRoute + "/edit-product-category";

        public const string DeleteProductCategory = BaseProductCategoryRoute + "/delete-product-category/{id}";
    }

    public static class Product
    {
        private const string BaseProductRoute = BaseRoute + "/product";

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
        private const string BaseProductPictureRoute = BaseRoute + "/product-picture";

        public const string FilterProductPictures = BaseProductPictureRoute + "/filter-product-pictures";

        public const string GetProductPictureDetails = BaseProductPictureRoute + "/{id}";

        public const string CreateProductPicture = BaseProductPictureRoute + "/create-product-picture";

        public const string EditProductPicture = BaseProductPictureRoute + "/edit-product-picture";

        public const string RemoveProductPicture = BaseProductPictureRoute + "/remove-product-picture/{id}";

    }
}