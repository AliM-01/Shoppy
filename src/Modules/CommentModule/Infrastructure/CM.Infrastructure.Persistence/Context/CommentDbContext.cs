using CM.Domain.Comment;
using CM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

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

        MongoCredential credential = MongoCredential.CreateCredential(_settings.DbName, _settings.User, _settings.Password);
        var mongoSettings = new MongoClientSettings
        {
            Credential = credential,
            Server = new MongoServerAddress(_settings.Host),
            ConnectTimeout = TimeSpan.FromMinutes(3),
            RetryWrites = true,
            RetryReads = true,
            DirectConnection = true
        };

        var client = new MongoClient(mongoSettings);

        var db = client.GetDatabase(_settings.DbName);

        Comments = db.GetCollection<Comment>(_settings.CommentCollection);
        CommentDbContextSeed.SeedData(Comments);
    }

    #endregion

    public IMongoCollection<Comment> Comments { get; set; }

}

public static class CommentDbContextSeed
{
    public static void SeedData(IMongoCollection<Comment> comments)
    {
        bool existsComment = comments.Find(_ => true).Any();

        if (!existsComment)
        {
            List<Comment> commentToAdd = new()
            {
                new Comment
                {
                    Name = "A",
                    Email = "a@gmail.com",
                    OwnerRecordId = 21,
                    Text = "<p>عالی بود</p>",
                    Type = CommentType.Product,
                    State = CommentState.Confirmed,
                    ParentId = null
                },
                new Comment
                {
                    Name = "B",
                    Email = "b@gmail.com",
                    OwnerRecordId = 21,
                    Text = "<p>جالب بود</p>",
                    Type = CommentType.Product,
                    State = CommentState.Confirmed,
                    ParentId = null
                },
                new Comment
                {
                    Name = "C",
                    Email = "c@gmail.com",
                    OwnerRecordId = 22,
                    Text = "<p>خوب بود</p>",
                    Type = CommentType.Product,
                    State = CommentState.Confirmed,
                    ParentId = null
                }
            };
            comments.InsertManyAsync(commentToAdd);
        }
    }
}