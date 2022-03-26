using System.Threading.Tasks;

namespace DM.Application.Contracts.Sevices;
public interface IDMProucAclService
{
    Task<bool> ExistsProductDiscount(string productId);
}
