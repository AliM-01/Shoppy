using System.Threading.Tasks;

namespace CM.Application.Contracts.Sevices;

public interface ICMProductAcl
{
    Task<string> GetProductTitle(string productId);
}
