using _0_Framework.Domain;
using MongoDB.Driver;

namespace _0_Framework.Infrastructure;

public static class MongoDbFilters<T> where T : EntityBase
{
    public static FilterDefinition<T> GetByIdFilter(string id)
    {
        return Builders<T>.Filter.Eq(x => x.Id, id);
    }

    public static FilterDefinition<T> GetBySlugFilter(string slug)
    {
        return Builders<T>.Filter.Eq("slug", slug);
    }
}
