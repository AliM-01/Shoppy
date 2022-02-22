using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SM.Domain.Product;
using SM.Domain.ProductCategory;
using SM.Domain.ProductFeature;
using SM.Domain.ProductPicture;
using SM.Domain.Slider;
using SM.Infrastructure.Persistence.Settings;

namespace SM.Infrastructure.Persistence.Context;

public interface IShopDbContext
{
    IMongoCollection<ProductCategory> ProductCategories { get; }
    IMongoCollection<Product> Products { get; }
    IMongoCollection<ProductPicture> ProductPictures { get; }
    IMongoCollection<ProductFeature> ProductFeatures { get; }
    IMongoCollection<Slider> Sliders { get; }
}

public class ShopDbContext : IShopDbContext
{
    #region ctor

    private readonly ShopDbSettings _settings;
    public ShopDbContext(IOptionsSnapshot<ShopDbSettings> settings)
    {
        _settings = settings.Value;

        var mongoSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
        mongoSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var client = new MongoClient(mongoSettings);

        var db = client.GetDatabase(_settings.DbName);

        ProductCategories = db.GetCollection<ProductCategory>(_settings.ProductCategoryCollection);
        Products = db.GetCollection<Product>(_settings.ProductCollection);
        ProductPictures = db.GetCollection<ProductPicture>(_settings.ProductPictureCollection);
        ProductFeatures = db.GetCollection<ProductFeature>(_settings.ProductFeatureCollection);
        Sliders = db.GetCollection<Slider>(_settings.SliderCollection);
    }

    #endregion

    public IMongoCollection<ProductCategory> ProductCategories { get; }

    public IMongoCollection<Product> Products { get; }

    public IMongoCollection<ProductPicture> ProductPictures { get; }

    public IMongoCollection<ProductFeature> ProductFeatures { get; }

    public IMongoCollection<Slider> Sliders { get; }
}
