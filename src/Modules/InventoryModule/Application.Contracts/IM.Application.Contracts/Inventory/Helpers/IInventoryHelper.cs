using System.Threading.Tasks;

namespace IM.Application.Contracts.Inventory.Helpers;

public interface IInventoryHelper
{
    Task<long> CalculateCurrentCount(Guid inventoryId);

    Task Increase(Guid inventoryId, long count, long operatorId, string description);

    Task Reduce(Guid inventoryId, long count, long operatorId, string description, long orderId);
}
