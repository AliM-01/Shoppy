using System.Threading.Tasks;

namespace IM.Application.Contracts.Inventory.Helpers;

public interface IInventoryHelper
{
    Task<long> CalculateCurrentCount(string inventoryId);

    Task Increase(string inventoryId, long count, long operatorId, string description);

    Task Reduce(string inventoryId, long count, long operatorId, string description, long orderId);
}
