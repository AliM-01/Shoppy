namespace _01_Shoppy.Query.Contracts.Product
{
    public interface IProductQuery
    {
        Task<Response<IEnumerable<ProductQueryModel>>> GetLatestProducts();
    }
}
