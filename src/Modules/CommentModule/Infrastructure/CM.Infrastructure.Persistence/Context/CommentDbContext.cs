using CM.Domain.Comment;
using CM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CM.Infrastructure.Persistence.Context;

public interface ICommentDbContext
{
    IMongoCollection<CM.Domain.Comment.Comment> Comments { get; }
}

public class CommentDbContext : ICommentDbContext
{
    #region ctor

    private readonly CommentDbSettings _settings;
    public CommentDbContext(IOptionsSnapshot<CommentDbSettings> settings)
    {
        _settings = settings.Value;

        var mongoSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        var db = client.GetDatabase(_settings.DbName);

        Comments = db.GetCollection<Comment>(_settings.CommentCollection);
    }

    #endregion

    public IMongoCollection<Comment> Comments { get; set; }

}
