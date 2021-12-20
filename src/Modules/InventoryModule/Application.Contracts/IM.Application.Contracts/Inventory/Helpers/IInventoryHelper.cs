using System.Threading.Tasks;

namespace IM.Application.Contracts.Inventory.Helpers;

public interface IInventoryHelper
{
    Task<long> CalculateCurrentCount(long inventoryId);

    Task Increase(long inventoryId, long count, long operatorId, string description);

    Task Reduce(long inventoryId, long count, long operatorId, string description, long orderId);
}
