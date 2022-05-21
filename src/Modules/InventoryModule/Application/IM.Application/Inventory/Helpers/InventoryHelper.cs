using IM.Application.Contracts.Inventory.Helpers;
using IM.Domain.Inventory;
using System.Linq;

namespace IM.Application.Inventory.Helpers;

public class InventoryHelper : IInventoryHelper
{
    #region Ctor

    private readonly IRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IRepository<InventoryOperation> _inventoryOperationHelper;

    public InventoryHelper(IRepository<Domain.Inventory.Inventory> inventoryRepository,
        IRepository<InventoryOperation> inventoryOperationHelper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _inventoryOperationHelper = Guard.Against.Null(inventoryOperationHelper, nameof(_inventoryOperationHelper));
    }


    #endregion

    #region IsInStock

    public async Task<bool> IsInStock(string inventoryId)
    {
        long count = await CalculateCurrentCount(inventoryId);

        return count > 0;
    }

    #endregion

    #region CalculateCurrentCount

    public async Task<long> CalculateCurrentCount(string inventoryId)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(inventoryId);

        NotFoundApiException.ThrowIfNull(inventory);

        long plus = _inventoryOperationHelper.AsQueryable()
                                             .Where(x => x.InventoryId == inventoryId && x.OperationType)
                                             .Sum(x => x.Count);

        long minus = _inventoryOperationHelper.AsQueryable()
                                              .Where(x => x.InventoryId == inventoryId && !x.OperationType)
                                              .Sum(x => x.Count);

        return (plus - minus);
    }

    #endregion

    #region Increase

    public async Task Increase(string inventoryId, long count, string operatorId, string description)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(inventoryId);

        NotFoundApiException.ThrowIfNull(inventory);

        long currentCount = (await CalculateCurrentCount(inventory.Id)) + count;

        var operation = new InventoryOperation(true, count, operatorId, currentCount, description, "0000-0000", inventoryId);

        await _inventoryOperationHelper.InsertAsync(operation);

        inventory.Operations.Add(operation);
        inventory.InStock = currentCount > 0;

        await _inventoryRepository.UpdateAsync(inventory);
    }

    #endregion

    #region Reduce

    public async Task Reduce(string inventoryId, long count, string operatorId, string description, string orderId)
    {
        var inventory = await _inventoryRepository.FindByIdAsync(inventoryId);

        NotFoundApiException.ThrowIfNull(inventory);

        long currentCount = (await CalculateCurrentCount(inventory.Id)) - count;

        var operation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, inventoryId);

        await _inventoryOperationHelper.InsertAsync(operation);

        inventory.Operations.Add(operation);
        inventory.InStock = currentCount > 0;

        await _inventoryRepository.UpdateAsync(inventory);
    }

    #endregion
}
