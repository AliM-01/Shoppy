using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Application.Contracts.Sevices;
public interface IDMProucAclService
{
    Task<bool> ExistsProduct(string productId);

    Task<bool> ExistsProductDiscount(string productId);

    /// <summary>
    /// Get Product Title
    /// </summary>
    /// <param name="productId">Product Id</param>
    /// <returns>Product Title</returns>
    Task<string> GetProductTitle(string productId);

    /// <summary>
    /// Filter Product Title
    /// </summary>
    /// <param name="filter">Product Title</param>
    /// <returns>HashSet of ProductIds</returns>
    Task<HashSet<string>> FilterTitle(string filter);
}
