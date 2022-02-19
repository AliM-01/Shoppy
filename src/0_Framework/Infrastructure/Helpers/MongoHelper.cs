using _0_Framework.Application.Exceptions;
using _0_Framework.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure.Helpers;

public class MongoHelper<TCollection, TDocument> : IMongoHelper<TDocument>
    where TCollection : IMongoCollection<TDocument>
    where TDocument : EntityBase
{
    #region Ctor

    private readonly TCollection _context;

    public MongoHelper(TCollection context)
    {
        _context = context;
    }

    #endregion

    #region GetQuery

    public IMongoQueryable<TDocument> GetQuery()
    {
        return _context.AsQueryable();
    }

    #endregion

    #region Exists

    public async Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> expression)
    {
        return await _context.AsQueryable().AnyAsync(expression);
    }

    #endregion

    #region GetByIdAsync

    public async Task<TDocument> GetByIdAsync(string id)
    {
        var filter = MongoDbFilters<TDocument>.GetByIdFilter(id);

        var res = await _context.FindAsync(filter);

        var document = await res.FirstOrDefaultAsync();

        if (document is null)
            throw new NotFoundApiException();

        return document;
    }

    #endregion

    #region Insert

    public async Task InsertAsync(TDocument document)
    {
        await _context.InsertOneAsync(document);
    }

    #endregion

    #region Update

    public async Task UpdateAsync(TDocument document)
    {
        document.LastUpdateDate = DateTime.UtcNow;

        var filter = MongoDbFilters<TDocument>.GetByIdFilter(document.Id);

        await _context.ReplaceOneAsync(filter, document);
    }

    #endregion

    #region Delete

    public async Task DeleteAsync(string id)
    {
        var document = await GetByIdAsync(id);

        document.IsDeleted = true;

        await UpdateAsync(document);
    }

    #endregion

    #region DeletePermanentAsync

    public async Task DeletePermanentAsync(string id)
    {
        var document = await GetByIdAsync(id);

        var filter = MongoDbFilters<TDocument>.GetByIdFilter(document.Id);

        await _context.DeleteOneAsync(filter);
    }

    #endregion
}
