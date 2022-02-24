using CM.Domain.Comment;
using MongoDB.Driver;
using SM.Infrastructure.Persistence.Seeds;
using System.Collections.Generic;

namespace CM.Infrastructure.Persistence.Context;

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
            comments.InsertManyAsync(commentToAdd);
        }
    }
}