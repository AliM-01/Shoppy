namespace _01_Shoppy.Query.Contracts.Product
{
    public interface IProductQuery
    {
        Task<Response<List<ProductQueryModel>>> GetHotestDiscountProducts();

        Task<Response<List<ProductQueryModel>>> GetLatestProducts();
    }
}
