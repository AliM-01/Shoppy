namespace _01_Shoppy.Query.Helpers.Product;

public interface IProductHelper
{
    Task<List<ProductQueryModel>> MapProducts(List<ProductQueryModel> products, bool hotDiscountQuery = false);

    Task<List<ProductQueryModel>> MapProductsFromProductCategories(List<SM.Domain.Product.Product> products);
}