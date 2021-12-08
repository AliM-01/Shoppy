namespace Shoppy.WebApi.Endpoints.Admin.Shop;

public static class AdminDiscountApiEndpoints
{
    private const string BaseAdminDiscountRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/discount";

    public static class CustomerDiscount
    {
        private const string BaseCustomerDiscountRoute = BaseAdminDiscountRoute + "/customer-discount";

        public const string FilterCustomerDiscounts = BaseCustomerDiscountRoute + "/filter-discounts";

        public const string GetCustomerDiscountDetails = BaseCustomerDiscountRoute + "/{id}";

        public const string DefineCustomerDiscount = BaseCustomerDiscountRoute + "/define-customer-discount";

        public const string EditCustomerDiscount = BaseCustomerDiscountRoute + "/edit-customer-discount";

        public const string RemoveCustomerDiscount = BaseCustomerDiscountRoute + "/remove-customer-discount/{id}";

        public const string CheckProductHasCustomerDiscount = BaseCustomerDiscountRoute + "/check-product-has-customer-discount/{productId}";

    }


    public static class ColleagueDiscount
    {
        private const string BaseColleagueDiscountRoute = BaseAdminDiscountRoute + "/colleague-discount";

        public const string FilterColleagueDiscounts = BaseColleagueDiscountRoute + "/filter-discounts";

        public const string GetColleagueDiscountDetails = BaseColleagueDiscountRoute + "/{id}";

        public const string DefineColleagueDiscount = BaseColleagueDiscountRoute + "/define-colleague-discount";

        public const string EditColleagueDiscount = BaseColleagueDiscountRoute + "/edit-colleague-discount";

        public const string RemoveColleagueDiscount = BaseColleagueDiscountRoute + "/remove-colleague-discount/{id}";

        public const string RestoreColleagueDiscount = BaseColleagueDiscountRoute + "/restore-colleague-discount/{id}";

        public const string DeleteColleagueDiscount = BaseColleagueDiscountRoute + "/delete-colleague-discount/{id}";

    }

}
