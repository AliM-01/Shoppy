using _0_Framework.Infrastructure;

namespace SM.Infrastructure;

public class ShopDbSettings : BaseDbSettings
{
    public string ProductCollection { get; set; }

    public string ProductCategoryCollection { get; set; }

    public string ProductPictureCollection { get; set; }

    public string ProductFeatureCollection { get; set; }

    public string SliderCollection { get; set; }
}

