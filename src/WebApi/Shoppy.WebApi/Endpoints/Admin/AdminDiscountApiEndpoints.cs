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


    public static class ColleagueDiscount
    {
        private const string BaseColleagueDiscountRoute = BaseAdminDiscountRoute + "/colleague-discount";

        public const string FilterColleagueDiscounts = BaseColleagueDiscountRoute + "/filter";

        public const string GetColleagueDiscountDetails = BaseColleagueDiscountRoute + "/{id}";

        public const string DefineColleagueDiscount = BaseColleagueDiscountRoute + "/define";

        public const string EditColleagueDiscount = BaseColleagueDiscountRoute + "/edit";

        public const string RemoveColleagueDiscount = BaseColleagueDiscountRoute + "/remove/{id}";

        public const string RestoreColleagueDiscount = BaseColleagueDiscountRoute + "/restore/{id}";

        public const string DeleteColleagueDiscount = BaseColleagueDiscountRoute + "/delete/{id}";

        public const string CheckProductHasColleagueDiscount = BaseColleagueDiscountRoute + "/check-product-has-discount/{productId}";

    }

}
