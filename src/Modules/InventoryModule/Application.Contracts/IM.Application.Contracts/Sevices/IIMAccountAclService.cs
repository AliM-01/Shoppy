using System.Threading.Tasks;

namespace IM.Application.Contracts.Sevices;
public interface IIMAccountAclService
{
    Task<string> GetFullName(string userId);
}
