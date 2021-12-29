namespace _01_Shoppy.Query.Contracts.ProductCategory
{
    public interface IProductQuery
    {
        Task<Response<IEnumerable<ProductQueryModel>>> GetLatestProducts();
    }
}
