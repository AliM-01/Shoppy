using System.Threading.Tasks;

namespace IM.Application.Contracts.Inventory.Helpers;

public interface IInventoryHelper
{
    Task<long> CalculateCurrentCount(long inventory);

    Task Increase(long inventory, long count, long operatorId, string description);

    Task Reduce(long inventory, long count, long operatorId, string description, long orderId);
}
