using _0_Framework.Infrastructure;

namespace IM.Infrastructure.Persistence.Settings;

public class InventoryDbSettings : BaseMongoDbSettings
{
    public string InventoryCollection { get; set; }

    public string InventoryOperationCollection { get; set; }
}
