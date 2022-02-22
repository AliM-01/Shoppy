using _0_Framework.Infrastructure;

namespace SM.Infrastructure.Persistence.Settings;

public class ShopDbSettings : BaseMongoDbSettings
{
    public string ProductCollection { get; set; }

    public string ProductCategoryCollection { get; set; }

    public string ProductPictureCollection { get; set; }

    public string ProductFeatureCollection { get; set; }

    public string SliderCollection { get; set; }
}

