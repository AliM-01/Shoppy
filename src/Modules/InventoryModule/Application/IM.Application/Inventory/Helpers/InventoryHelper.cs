using IM.Application.Contracts.Inventory.Helpers;
using IM.Domain.Inventory;
using System.Linq;

namespace IM.Application.Inventory.Helpers;

public class InventoryHelper : IInventoryHelper
{
    #region Ctor

    private readonly IMongoHelper<Domain.Inventory.Inventory> _inventoryHelper;
    private readonly IMongoHelper<InventoryOperation> _inventoryOperationHelper;

    public InventoryHelper(IMongoHelper<Domain.Inventory.Inventory> inventoryHelper,
        IMongoHelper<InventoryOperation> inventoryOperationHelper)
    {
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
        _inventoryOperationHelper = Guard.Against.Null(inventoryOperationHelper, nameof(_inventoryOperationHelper));
    }


    #endregion

    #region CalculateCurrentCount

    public async Task<long> CalculateCurrentCount(string inventoryId)
    {
        var inventory = await _inventoryHelper.GetByIdAsync(inventoryId);


        if (inventory is null)
            throw new NotFoundApiException();

        var plus = _inventoryOperationHelper.AsQueryable()
            .Where(x => x.InventoryId == inventoryId && x.OperationType).Sum(x => x.Count);
        var minus = _inventoryOperationHelper.AsQueryable()
            .Where(x => x.InventoryId == inventoryId && !x.OperationType).Sum(x => x.Count);
        return (plus - minus);
    }

    #endregion

    #region Increase

    public async Task Increase(string inventoryId, long count, long operatorId, string description)
    {
        var inventory = await _inventoryHelper.GetByIdAsync(inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var currentCount = await CalculateCurrentCount(inventory.Id);
        currentCount += count;

        var operation = new InventoryOperation(true, count, operatorId, currentCount, description, 0, inventoryId);

        await _inventoryOperationHelper.InsertAsync(operation);

        inventory.Operations.Add(operation);
        inventory.InStock = currentCount > 0;

        await _inventoryHelper.UpdateAsync(inventory);
    }

    #endregion

    #region Reduce

    public async Task Reduce(string inventoryId, long count, long operatorId, string description, long orderId)
    {
        var inventory = await _inventoryHelper.GetByIdAsync(inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var currentCount = await CalculateCurrentCount(inventory.Id);
        currentCount += count;

        var operation = new InventoryOperation(false, count, operatorId, currentCount, description, orderId, inventoryId);

        await _inventoryOperationHelper.InsertAsync(operation);

        inventory.Operations.Add(operation);
        inventory.InStock = currentCount > 0;

        await _inventoryHelper.UpdateAsync(inventory);
    }

    #endregion
}
