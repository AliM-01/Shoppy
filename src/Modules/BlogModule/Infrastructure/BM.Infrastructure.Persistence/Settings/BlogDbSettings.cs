using _0_Framework.Infrastructure;

namespace BM.Infrastructure.Persistence.Settings;

public class BlogDbSettings : BaseDbSettings
{
    public string ArticleCategoryCollection { get; set; }

    public string ArticleCollection { get; set; }
}
