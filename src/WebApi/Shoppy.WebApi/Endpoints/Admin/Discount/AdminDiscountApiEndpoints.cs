namespace Shoppy.WebApi.Endpoints.Admin.Shop;

public static class AdminDiscountApiEndpoints
{
    private const string BaseAdminDiscountRoute = BaseApiEndpointRoutes.AdminBaseRoute + "/discount";

    public static class CustomerDiscount
    {
        private const string BaseCustomerDiscountRoute = BaseAdminDiscountRoute + "/customer-discount";

        public const string FilterCustomerDiscounts = BaseCustomerDiscountRoute + "/filter-discounts";

        public const string GetCustomerDiscountDetails = BaseCustomerDiscountRoute + "/{id}";

    }

}
