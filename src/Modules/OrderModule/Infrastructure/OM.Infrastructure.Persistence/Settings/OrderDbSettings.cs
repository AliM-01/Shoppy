using _0_Framework.Infrastructure;

namespace OM.Infrastructure.Persistence.Settings;

public class OrderDbSettings : BaseDbSettings
{
    public string OrderCollection { get; set; }

    public string OrderItemCollection { get; set; }
}
