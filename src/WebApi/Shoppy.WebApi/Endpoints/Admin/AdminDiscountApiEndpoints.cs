namespace Shoppy.WebApi.Endpoints.Admin;

public static class AdminDiscountApiEndpoints
{
    private const string BaseAdminDiscountRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/discount";

    public static class ProductDiscount
    {
        private const string BaseProductDiscountRoute = BaseAdminDiscountRoute + "/product-discount";

        public const string FilterProductDiscounts = BaseProductDiscountRoute + "/filter";

        public const string GetProductDiscountDetails = BaseProductDiscountRoute + "/{id}";

        public const string DefineProductDiscount = BaseProductDiscountRoute + "/define";

        public const string EditProductDiscount = BaseProductDiscountRoute + "/edit";

        public const string RemoveProductDiscount = BaseProductDiscountRoute + "/remove/{id}";

        public const string CheckProductHasProductDiscount = BaseProductDiscountRoute + "/check-product-has-discount/{productId}";

    }

}
