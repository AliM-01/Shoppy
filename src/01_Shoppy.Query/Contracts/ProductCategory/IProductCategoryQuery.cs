namespace _01_Shoppy.Query.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        Task<Response<IEnumerable<ProductCategoryQueryModel>>> GetProductCategories();

        Task<Response<ProductCategoryQueryModel>> GetProductCategoryWithProductsBy(ProductCategoryDetailsFilterModel filter);
    }
}
