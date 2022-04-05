using System.Threading.Tasks;

namespace CM.Application.Contracts.Sevices;

public interface ICMArticleAcl
{
    Task<string> GetArticleTitle(string articleId);
}
