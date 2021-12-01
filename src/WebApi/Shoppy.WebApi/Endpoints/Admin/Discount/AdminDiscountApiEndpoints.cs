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

        public const string RemoveCustomerDiscount = BaseCustomerDiscountRoute + "/delete-product-category/{id}";

    }

}
