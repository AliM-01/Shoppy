using _0_Framework.Domain.Attributes;
using MongoDB.Driver;
using System.Linq;

namespace _0_Framework.Infrastructure;

public static class MongoDbConnector
{
    public static IMongoCollection<TDocument> Conncet<TDocument>(BaseMongoDbSettings dbSettings)
    {
        var mongoSettings = MongoClientSettings.FromConnectionString(dbSettings.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        var database = client.GetDatabase(dbSettings.DbName);

        return database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
    }

    private static string GetCollectionName(Type documentType)
    {
        return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
    }
}
