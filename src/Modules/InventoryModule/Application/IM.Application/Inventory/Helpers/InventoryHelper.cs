using IM.Application.Contracts.Inventory.Helpers;
using IM.Domain.Inventory;
using System.Linq;

namespace IM.Application.Inventory.Helpers;

public class InventoryHelper : IInventoryHelper
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;

    public InventoryHelper(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
    }

    #endregion

    public async Task<long> CalculateCurrentCount(long inventoryId)
    {
        var inventory = await _inventoryRepository.GetQuery()
            .Include(x => x.Operations)
            .FirstOrDefaultAsync(x => x.Id == inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var plus = inventory.Operations.Where(x => x.OperationType).Sum(x => x.Count);
        var minus = inventory.Operations.Where(x => !x.OperationType).Sum(x => x.Count);
        return (plus - minus);
    }

    public async Task Increase(long inventoryId, long count, long operatorId, string description)
    {
        var inventory = await _inventoryRepository.GetQuery()
            .Include(x => x.Operations)
            .FirstOrDefaultAsync(x => x.Id == inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var currentCount = await CalculateCurrentCount(inventory.Id);
        currentCount += count;

        inventory.Operations.Add(new InventoryOperation(true, count, operatorId, currentCount,
                        description, 0, inventoryId));

        inventory.InStock = currentCount > 0;
        _inventoryRepository.Update(inventory);
        await _inventoryRepository.SaveChanges();
    }

    public async Task Reduce(long inventoryId, long count, long operatorId, string description, long orderId)
    {
        var inventory = await _inventoryRepository.GetQuery()
             .Include(x => x.Operations)
             .FirstOrDefaultAsync(x => x.Id == inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var currentCount = await CalculateCurrentCount(inventory.Id);
        currentCount -= count;

        inventory.Operations.Add(new InventoryOperation(false, count, operatorId, currentCount,
                        description, orderId, inventoryId));

        inventory.InStock = currentCount > 0;
        _inventoryRepository.Update(inventory);
        await _inventoryRepository.SaveChanges();
    }
}
