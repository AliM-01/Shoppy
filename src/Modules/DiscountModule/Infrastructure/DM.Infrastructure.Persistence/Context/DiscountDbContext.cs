using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;
using DM.Infrastructure.Persistence.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DM.Infrastructure.Persistence.Context;

public interface IDiscountDbContext
{
    IMongoCollection<ProductDiscount> ProductDiscounts { get; }
    IMongoCollection<DiscountCode> DiscountCodes { get; }
}

public class DiscountDbContext : IDiscountDbContext
{
    #region ctor

    private readonly DiscountDbSettings _settings;
    public DiscountDbContext(IOptionsSnapshot<DiscountDbSettings> settings)
    {
        _settings = settings.Value;

        var mongoSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        var db = client.GetDatabase(_settings.DbName);

        ProductDiscounts = db.GetCollection<ProductDiscount>(_settings.ProductDiscountCollection);
        DiscountCodes = db.GetCollection<DiscountCode>(_settings.DiscountCodeCollection);
    }

    #endregion

    public IMongoCollection<ProductDiscount> ProductDiscounts { get; }
    public IMongoCollection<DiscountCode> DiscountCodes { get; }
}