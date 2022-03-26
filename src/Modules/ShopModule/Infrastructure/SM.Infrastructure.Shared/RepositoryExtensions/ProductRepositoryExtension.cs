using _0_Framework.Infrastructure.IRepository;
using SM.Domain.Product;
using System.Threading.Tasks;

namespace SM.Infrastructure.Shared.RepositoryExtensions;

public static class ProductRepositoryExtension
{
    public static async Task<bool> ExistsProduct(this IRepository<Product> productRepository, string productId)
    {
        return await productRepository.ExistsAsync(p => p.Id == productId);
    }
}
