using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using BM.Infrastructure.Persistence.Seed;
using BM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BM.Infrastructure.Persistence.Context;

public interface IBlogDbContext
{
    IMongoCollection<ArticleCategory> ArticleCategories { get; }

    IMongoCollection<Article> Articles { get; }
}

public class BlogDbContext : IBlogDbContext
{
    #region ctor

    private readonly BlogDbSettings _settings;
    public BlogDbContext(IOptionsSnapshot<BlogDbSettings> settings)
    {
        _settings = settings.Value;

        var mongoSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        var db = client.GetDatabase(_settings.DbName);

        ArticleCategories = db.GetCollection<ArticleCategory>(_settings.ArticleCategoryCollection);
        Articles = db.GetCollection<Article>(_settings.ArticleCollection);

        var categories = BlogDbDataSeed.SeedArticleCategoryData(ArticleCategories);
        BlogDbDataSeed.SeedArticleData(Articles, categories);
    }

    #endregion

    public IMongoCollection<ArticleCategory> ArticleCategories { get; set; }

    public IMongoCollection<Article> Articles { get; set; }
}
