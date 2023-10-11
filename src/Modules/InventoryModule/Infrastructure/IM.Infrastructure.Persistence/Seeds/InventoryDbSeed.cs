using _0_Framework.Infrastructure;
using AM.Infrastructure.Seed;
using IM.Domain.Inventory;
using IM.Infrastructure.Persistence.Settings;
using MongoDB.Driver;
using SM.Infrastructure.Persistence.Seeds;
using System.Collections.Generic;

namespace IM.Infrastructure.Persistence.Seeds;

public static class InventoryDbSeed
{
    public static Inventory[] SeedInventories(InventoryDbSettings dbSettings)
    {
        var collection = MongoDbConnection.Conncet<Inventory>(dbSettings);

        bool existsInventory = collection.Find(_ => true).Any();

        if (!existsInventory)
        {
            Inventory[] inventoryToAdd =
            {
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_01,
                    UnitPrice = 10300000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_02,
                    UnitPrice = 25000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_03,
                    UnitPrice = 6000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_04,
                    UnitPrice = 10000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_05,
                    UnitPrice = 7000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_06,
                    UnitPrice = 11000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    UnitPrice = 15000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_08,
                    UnitPrice = 13000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_09,
                    UnitPrice = 9000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    UnitPrice = 23000000
                },
                new Inventory
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    UnitPrice = 30000000
                }
            };
            collection.InsertManyAsync(inventoryToAdd);

            return inventoryToAdd;
        }

        return null;
    }

    public static void SeedInventoryOperations(InventoryDbSettings dbSettings, Inventory[] inventories)
    {
        if (inventories is null)
            return;

        var collection = MongoDbConnection.Conncet<InventoryOperation>(dbSettings);

        bool existsInventory = collection.Find(_ => true).Any();

        if (!existsInventory)
        {
            const string Increase = "افزایش";
            const string Reduce = "کاهش";
            const string orderId = "0";
            const string operatorId = SeedUserIdConstants.BasicUser;

            List<InventoryOperation> operationToAdd = new()
            {
                new InventoryOperation(true, 100, operatorId, 100, Increase, orderId, inventories[0].Id),
                new InventoryOperation(false, 50, operatorId, 50, Reduce, orderId, inventories[0].Id),

                new InventoryOperation(true, 20, operatorId, 20, Increase, orderId, inventories[1].Id),
                new InventoryOperation(false, 3, operatorId, 17, Reduce, orderId, inventories[1].Id),
                new InventoryOperation(true, 10, operatorId, 10, Increase, orderId, inventories[2].Id),
                new InventoryOperation(true, 5, operatorId, 5, Increase, orderId, inventories[3].Id),
                new InventoryOperation(true, 25, operatorId, 25, Increase, orderId, inventories[4].Id),
                new InventoryOperation(true, 20, operatorId, 20, Increase, orderId, inventories[5].Id),
                new InventoryOperation(true, 50, operatorId, 50, Increase, orderId, inventories[6].Id),
                new InventoryOperation(true, 10, operatorId, 10, Increase, orderId, inventories[7].Id),
                new InventoryOperation(true, 25, operatorId, 25, Increase, orderId, inventories[8].Id),
                new InventoryOperation(true, 9, operatorId, 9, Increase, orderId, inventories[9].Id),
                new InventoryOperation(true, 7, operatorId, 7, Increase, orderId, inventories[10].Id),
            };

            collection.InsertManyAsync(operationToAdd);
        }
    }
}
