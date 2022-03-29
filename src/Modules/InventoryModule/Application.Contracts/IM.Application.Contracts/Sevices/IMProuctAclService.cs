using System.Collections.Generic;
using System.Threading.Tasks;

namespace IM.Application.Contracts.Sevices;
public interface IIMProuctAclService
{
    Task<bool> ExistsProduct(string productId);

    Task<bool> ExistsInventory(string productId);

    Task<string> GetProductTitle(string productId);

    Task<HashSet<string>> FilterTitle(string filter);
}
