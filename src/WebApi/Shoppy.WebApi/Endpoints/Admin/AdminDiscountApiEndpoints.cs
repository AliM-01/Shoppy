namespace Shoppy.WebApi.Endpoints.Admin;

public static class AdminDiscountApiEndpoints
{
    private const string BaseAdminDiscountRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/discount";

    public static class CustomerDiscount
    {
        private const string BaseCustomerDiscountRoute = BaseAdminDiscountRoute + "/customer-discount";

        public const string FilterCustomerDiscounts = BaseCustomerDiscountRoute + "/filter";

        public const string GetCustomerDiscountDetails = BaseCustomerDiscountRoute + "/{id}";

        public const string DefineCustomerDiscount = BaseCustomerDiscountRoute + "/define";

        public const string EditCustomerDiscount = BaseCustomerDiscountRoute + "/edit";

        public const string RemoveCustomerDiscount = BaseCustomerDiscountRoute + "/remove/{id}";

        public const string CheckProductHasCustomerDiscount = BaseCustomerDiscountRoute + "/check-product-has-discount/{productId}";

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
