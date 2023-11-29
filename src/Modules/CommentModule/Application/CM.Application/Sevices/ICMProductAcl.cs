using System.Threading.Tasks;

namespace CM.Application.Sevices;

public interface ICMProductAcl
{
    Task<string> GetProductTitle(string productId);
}
