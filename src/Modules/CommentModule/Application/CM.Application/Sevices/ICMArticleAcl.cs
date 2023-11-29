using System.Threading.Tasks;

namespace CM.Application.Sevices;

public interface ICMArticleAcl
{
    Task<string> GetArticleTitle(string articleId);
}
