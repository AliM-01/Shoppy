using IM.Application.Contracts.Inventory.Helpers;
using IM.Domain.Inventory;
using System;
using System.Linq;

namespace IM.Application.Inventory.Helpers;

public class InventoryHelper : IInventoryHelper
{
    #region Ctor

    private readonly IGenericRepository<Domain.Inventory.Inventory> _inventoryRepository;
    private readonly IGenericRepository<InventoryOperation> _inventoryOperationRepository;

    public InventoryHelper(IGenericRepository<Domain.Inventory.Inventory> inventoryRepository,
        IGenericRepository<InventoryOperation> inventoryOperationRepository)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _inventoryOperationRepository = Guard.Against.Null(inventoryOperationRepository, nameof(_inventoryOperationRepository));
    }


    #endregion

    #region CalculateCurrentCount

    public async Task<long> CalculateCurrentCount(Guid inventoryId)
    {
        var inventory = await _inventoryRepository.GetQuery().AsTracking()
            .FirstOrDefaultAsync(x => x.Id == inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var plus = _inventoryOperationRepository.GetQuery().AsQueryable()
            .Where(x => x.InventoryId == inventoryId && x.OperationType).Sum(x => x.Count);
        var minus = _inventoryOperationRepository.GetQuery().AsQueryable()
            .Where(x => x.InventoryId == inventoryId && !x.OperationType).Sum(x => x.Count);
        return (plus - minus);
    }

    #endregion

    #region Increase

    public async Task Increase(Guid inventoryId, long count, long operatorId, string description)
    {
        var inventory = await _inventoryRepository.GetQuery().AsTracking()
            .FirstOrDefaultAsync(x => x.Id == inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var currentCount = await CalculateCurrentCount(inventory.Id);
        currentCount += count;

        await _inventoryOperationRepository.InsertEntity(
            new InventoryOperation(true, count, operatorId, currentCount, description, 0, inventoryId));
        await _inventoryOperationRepository.SaveChanges();

        inventory.InStock = currentCount > 0;
        _inventoryRepository.Update(inventory);
        await _inventoryRepository.SaveChanges();
    }

    #endregion

    #region Reduce

    public async Task Reduce(Guid inventoryId, long count, long operatorId, string description, long orderId)
    {
        var inventory = await _inventoryRepository.GetQuery().AsTracking()
            .FirstOrDefaultAsync(x => x.Id == inventoryId);

        if (inventory is null)
            throw new NotFoundApiException();

        var currentCount = await CalculateCurrentCount(inventory.Id);
        currentCount -= count;

        await _inventoryOperationRepository.InsertEntity(
            new InventoryOperation(false, count, operatorId, currentCount, description, orderId, inventoryId));
        await _inventoryOperationRepository.SaveChanges();

        inventory.InStock = currentCount > 0;
        _inventoryRepository.Update(inventory);
        await _inventoryRepository.SaveChanges();
    }

    #endregion
}
