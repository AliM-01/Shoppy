using _0_Framework.Domain;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure.Helpers;

public interface IMongoHelper<TDocument>
    where TDocument : EntityBase
{
    IMongoQueryable<TDocument> GetQuery();

    Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> expression);

    Task<TDocument> GetByIdAsync(string id);

    Task InsertAsync(TDocument document);

    Task UpdateAsync(TDocument document);

    Task DeleteAsync(string id);

    Task DeletePermanentAsync(string id);
}
