using _0_Framework.Infrastructure;
using CM.Domain.Comment;
using CM.Infrastructure.Persistence.Settings;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CM.Infrastructure.Persistence.Context;

public static class CommentDbContextSeed
{
    public static void SeedData(CommentDbSettings dbSettings)
    {
        var collection = MongoDbConnection.Conncet<Comment>(dbSettings);

        bool existsComment = collection.Find(_ => true).Any();

        if (!existsComment)
        {
            List<Comment> commentToAdd = new()
            {
                new Comment
                {
                    Name = "A",
                    Email = "a@gmail.com",
                    OwnerRecordId = SeedProductIdConstants.Product_01,
                    Text = "<p>عالی بود</p>",
                    Type = CommentType.Product,
                    State = CommentState.Confirmed,
                    ParentId = null
                },
                new Comment
                {
                    Name = "B",
                    Email = "b@gmail.com",
                    OwnerRecordId = SeedProductIdConstants.Product_09,
                    Text = "<p>جالب بود</p>",
                    Type = CommentType.Product,
                    State = CommentState.Confirmed,
                    ParentId = null
                },
                new Comment
                {
                    Name = "C",
                    Email = "c@gmail.com",
                    OwnerRecordId = SeedProductIdConstants.Product_02,
                    Text = "<p>خوب بود</p>",
                    Type = CommentType.Product,
                    State = CommentState.Confirmed,
                    ParentId = null
                },
                new Comment
                {
                    Name = "D",
                    Email = "d@gmail.com",
                    OwnerRecordId = SeedProductIdConstants.Product_07,
                    Text = "<p>قیمت مناسب</p>",
                    Type = CommentType.Product,
                    State = CommentState.Confirmed,
                    ParentId = null
                }
            };
            collection.InsertManyAsync(commentToAdd);
        }
    }
}