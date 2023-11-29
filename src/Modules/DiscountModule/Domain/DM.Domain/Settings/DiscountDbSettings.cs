using _0_Framework.Infrastructure;

namespace DM.Domain.Settings;

public class DiscountDbSettings : BaseDbSettings
{
    public string ProductDiscountCollection { get; set; }

    public string DiscountCodeCollection { get; set; }
}
