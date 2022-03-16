using System.Threading.Tasks;

namespace IM.Application.Contracts.Inventory.Helpers;

public interface IInventoryHelper
{
    Task<bool> IsInStock(string inventoryId);

    Task<long> CalculateCurrentCount(string inventoryId);

    Task Increase(string inventoryId, long count, string operatorId, string description);

    Task Reduce(string inventoryId, long count, string operatorId, string description, string orderId);
}
