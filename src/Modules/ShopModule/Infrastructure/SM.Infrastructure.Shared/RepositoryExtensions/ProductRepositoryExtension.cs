using _0_Framework.Infrastructure.IRepository;
using SM.Domain.Product;
using System.Threading.Tasks;

namespace SM.Infrastructure.Shared.RepositoryExtensions;

public static class ProductRepositoryExtension
{
    #region ExistsProduct

    public static async Task<bool> ExistsProduct(this IRepository<Product> productRepository, string productId)
    {
        return await productRepository.ExistsAsync(p => p.Id == productId);
    }

    #endregion

    #region GetProductTitle

    public static async Task<string> GetProductTitle(this IRepository<Product> productRepository, string productId)
    {
        return (await productRepository.FindByIdAsync(productId)).Title;
    }

    #endregion
}
