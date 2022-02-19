using _0_Framework.Application.Exceptions;
using _0_Framework.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure.Helpers;

public class MongoHelper<TDocument, TSettings> : IMongoHelper<TDocument>
    where TDocument : EntityBase
    where TSettings : BaseMongoDbSettings
{
    #region Ctor

    private readonly IMongoCollection<TDocument> _context;
    private readonly TSettings _settings;

    public MongoHelper(IOptionsSnapshot<TSettings> settings)
    {
        _settings = settings.Value;

        var mongoSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        var database = client.GetDatabase(_settings.DbName);

        _context = database.GetCollection<TDocument>(nameof(TDocument) + "Collection");
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
