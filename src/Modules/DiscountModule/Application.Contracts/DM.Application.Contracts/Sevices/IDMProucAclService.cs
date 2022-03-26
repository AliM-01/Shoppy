using System.Collections.Generic;
using System.Threading.Tasks;

namespace DM.Application.Contracts.Sevices;
public interface IDMProucAclService
{
    Task<bool> ExistsProduct(string productId);

    Task<bool> ExistsProductDiscount(string productId);

    Task<string> GetProductTitle(string productId);

    Task<HashSet<string>> GetProductIdsForFilterTitle(string filter);
}
