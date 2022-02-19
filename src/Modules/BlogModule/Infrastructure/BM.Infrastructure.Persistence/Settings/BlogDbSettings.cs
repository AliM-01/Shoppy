namespace BM.Infrastructure.Persistence.Settings;

public class BlogDbSettings
{
    public string ConnectionString { get; set; }

    public string DbName { get; set; }

    public string ArticleCategoryCollection { get; set; }

    public string ArticleCollection { get; set; }
}
