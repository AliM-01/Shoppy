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

    public static class DiscountCode
    {
        private const string BaseDiscountCodeRoute = BaseAdminDiscountRoute + "/discount-code";

        public const string FilterDiscountCodes = BaseDiscountCodeRoute + "/filter";

        public const string GetDiscountCodeDetails = BaseDiscountCodeRoute + "/{id}";

        public const string DefineDiscountCode = BaseDiscountCodeRoute + "/define";

        public const string EditDiscountCode = BaseDiscountCodeRoute + "/edit";

        public const string RemoveDiscountCode = BaseDiscountCodeRoute + "/remove/{id}";
    }
}
