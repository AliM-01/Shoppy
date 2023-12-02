namespace ServiceHost.Endpoints.Admin;

public static class AdminDiscountEndpoints
{
    private const string Base = BaseApiEndpointRoutes.AdminBaseRoute + "/discount";

    public static class ProductDiscount
    {
        private const string BaseProductDiscount = Base + "/product-discount";

        public const string FilterProductDiscounts = BaseProductDiscount + "/filter";

        public const string GetProductDiscountDetails = BaseProductDiscount + "/{id}";

        public const string DefineProductDiscount = BaseProductDiscount;

        public const string EditProductDiscount = BaseProductDiscount;

        public const string RemoveProductDiscount = BaseProductDiscount + "/{id}";

        public const string CheckProductHasProductDiscount = BaseProductDiscount + "/has-discount/{productId}";
    }

    public static class DiscountCode
    {
        private const string BaseDiscountCode = Base + "/discount-code";

        public const string FilterDiscountCodes = BaseDiscountCode + "/filter";

        public const string GetDiscountCodeDetails = BaseDiscountCode + "/{id}";

        public const string DefineDiscountCode = BaseDiscountCode;

        public const string EditDiscountCode = BaseDiscountCode;

        public const string RemoveDiscountCode = BaseDiscountCode + "/{id}";
    }
}