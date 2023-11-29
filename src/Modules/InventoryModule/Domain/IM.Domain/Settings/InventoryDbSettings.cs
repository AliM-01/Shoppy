using _0_Framework.Infrastructure;

namespace IM.Domain.Settings;

public class InventoryDbSettings : BaseDbSettings
{
    public string InventoryCollection { get; set; }

    public string InventoryOperationCollection { get; set; }
}
