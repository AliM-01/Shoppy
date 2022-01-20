namespace _01_Shoppy.Query.Contracts.Product;

public interface IProductQuery
{
    Task<Response<List<ProductQueryModel>>> GetHotestDiscountProducts();

    Task<Response<List<ProductQueryModel>>> GetLatestProducts();

    Task<Response<SearchProductQueryModel>> Search(SearchProductQueryModel search);

    Task<Response<ProductDetailsQueryModel>> GetProductDetails(string slug);
}
