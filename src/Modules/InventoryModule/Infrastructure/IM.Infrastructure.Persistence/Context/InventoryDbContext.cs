using IM.Domain.Inventory;
using IM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IM.Infrastructure.Persistence.Context;

public interface IInventoryDbContext
{
    IMongoCollection<Inventory> Inventories { get; set; }

    IMongoCollection<InventoryOperation> InventoryOperations { get; set; }
}

public class InventoryDbContext : IInventoryDbContext
{
    #region ctor

    private readonly InventoryDbSettings _settings;
    public InventoryDbContext(IOptionsSnapshot<InventoryDbSettings> settings)
    {
        _settings = settings.Value;

        var mongoSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        var db = client.GetDatabase(_settings.DbName);

        Inventories = db.GetCollection<Inventory>(_settings.InventoryCollection);
        InventoryOperations = db.GetCollection<InventoryOperation>(_settings.InventoryOperationCollection);
    }

    #endregion


    public IMongoCollection<Inventory> Inventories { get; set; }
    public IMongoCollection<InventoryOperation> InventoryOperations { get; set; }

}