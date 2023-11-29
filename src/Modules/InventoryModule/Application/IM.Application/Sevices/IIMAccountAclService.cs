using System.Threading.Tasks;

namespace IM.Application.Sevices;
public interface IIMAccountAclService
{
    Task<string> GetFullName(string userId);
}
