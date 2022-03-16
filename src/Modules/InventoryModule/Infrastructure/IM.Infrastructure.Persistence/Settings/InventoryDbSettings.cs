using _0_Framework.Infrastructure;

namespace IM.Infrastructure.Persistence.Settings;

public class InventoryDbSettings : BaseDbSettings
{
    public string InventoryCollection { get; set; }

    public string InventoryOperationCollection { get; set; }
}
