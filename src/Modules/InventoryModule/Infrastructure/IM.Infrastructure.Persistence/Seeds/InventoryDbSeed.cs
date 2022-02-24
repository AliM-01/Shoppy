using IM.Domain.Inventory;
using MongoDB.Driver;
using SM.Infrastructure.Persistence.Seeds;
using System.Collections.Generic;

namespace IM.Infrastructure.Persistence.Seeds;

public static class InventoryDbSeed
{
    public static Inventory[] SeedInventories(IMongoCollection<Inventory> inventorys)
    {
        bool existsInventory = inventorys.Find(_ => true).Any();

        if (!existsInventory)
        {
            Inventory[] inventoryToAdd =
            {
                new Inventory(SeedProductIdConstants.Product_01, 10300000),
                new Inventory(SeedProductIdConstants.Product_02, 25000000),
                new Inventory(SeedProductIdConstants.Product_03, 6000000),
                new Inventory(SeedProductIdConstants.Product_04, 10000000),
                new Inventory(SeedProductIdConstants.Product_05, 7000000),
                new Inventory(SeedProductIdConstants.Product_06, 11000000),
                new Inventory(SeedProductIdConstants.Product_07, 15000000),
                new Inventory(SeedProductIdConstants.Product_08, 13000000),
                new Inventory(SeedProductIdConstants.Product_09, 9000000),
                new Inventory(SeedProductIdConstants.Product_10, 23000000),
                new Inventory(SeedProductIdConstants.Product_11, 30000000),
            };
            inventorys.InsertManyAsync(inventoryToAdd);

            return inventoryToAdd;
        }

        return null;
    }

    public static void SeedInventoryOperations(IMongoCollection<InventoryOperation> operations, Inventory[] inventories)
    {
        bool existsInventory = operations.Find(_ => true).Any();

        if (!existsInventory)
        {
            const string Increase = "افزایش";
            const string Reduce = "کاهش";

            List<InventoryOperation> operationToAdd = new()
            {
                new InventoryOperation(true, 100, 0, 100, Increase, 0, inventories[0].Id),
                new InventoryOperation(false, 50, 0, 50, Reduce, 0, inventories[0].Id),

                new InventoryOperation(true, 20, 0, 20, Increase, 0, inventories[1].Id),
                new InventoryOperation(false, 3, 0, 17, Reduce, 0, inventories[1].Id),
                new InventoryOperation(true, 10, 0, 10, Increase, 0, inventories[2].Id),
                new InventoryOperation(true, 5, 0, 5, Increase, 0, inventories[3].Id),
                new InventoryOperation(true, 25, 0, 25, Increase, 0, inventories[4].Id),
                new InventoryOperation(true, 20, 0, 20, Increase, 0, inventories[5].Id),
                new InventoryOperation(true, 50, 0, 50, Increase, 0, inventories[6].Id),
                new InventoryOperation(true, 10, 0, 10, Increase, 0, inventories[7].Id),
                new InventoryOperation(true, 25, 0, 25, Increase, 0, inventories[8].Id),
                new InventoryOperation(true, 9, 0, 9, Increase, 0, inventories[9].Id),
                new InventoryOperation(true, 7, 0, 7, Increase, 0, inventories[10].Id),
                new InventoryOperation(true, 3, 0, 3, Increase, 0, inventories[11].Id),
            };

            operations.InsertManyAsync(operationToAdd);
        }
    }
}
