namespace _01_Shoppy.Query.Helpers.Product;

public interface IProductHelper
{
    Task<ProductQueryModel> MapProducts(ProductQueryModel products, bool hotDiscountQuery = false);

    Task<ProductQueryModel> MapProductsFromProductCategories(SM.Domain.Product.Product product);
}