using _0_Framework.Infrastructure;

namespace BM.Infrastructure.Persistence.Settings;

public class BlogDbSettings : BaseMongoDbSettings
{
    public string ArticleCategoryCollection { get; set; }

    public string ArticleCollection { get; set; }
}
