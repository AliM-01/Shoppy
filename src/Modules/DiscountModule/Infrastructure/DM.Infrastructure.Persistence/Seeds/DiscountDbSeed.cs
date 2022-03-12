using _0_Framework.Application.Extensions;
using _0_Framework.Infrastructure;
using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;
using DM.Infrastructure.Persistence.Settings;
using MongoDB.Driver;
using SM.Infrastructure.Persistence.Seeds;

namespace DM.Infrastructure.Persistence.Seeds;

public static class DiscountDbSeed
{
    public static void SeedDiscountCodes(DiscountDbSettings dbSettings)
    {
        var collection = MongoDbConnector.Conncet<DiscountCode>(dbSettings);

        bool existsDiscount = collection.Find(_ => true).Any();

        if (!existsDiscount)
        {
            const string description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ";

            DiscountCode[] discountToAdd =
            {
                new DiscountCode
                {
                    Description = description,
                    Code = Generators.GenerateCode(),
                    Rate = 10,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new DiscountCode
                {
                    Description = description,
                    Code = Generators.GenerateCode(),
                    Rate = 75,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new DiscountCode
                {
                    Description = description,
                    Code = Generators.GenerateCode(),
                    Rate = 50,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new DiscountCode
                {
                    Description = description,
                    Code = Generators.GenerateCode(),
                    Rate = 99,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                }
            };
            collection.InsertManyAsync(discountToAdd);
        }
    }

    public static void SeedProductDiscounts(DiscountDbSettings dbSettings)
    {
        var collection = MongoDbConnector.Conncet<ProductDiscount>(dbSettings);

        bool existsDiscount = collection.Find(_ => true).Any();

        if (!existsDiscount)
        {
            ProductDiscount[] inventoryToAdd =
            {
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_01,
                    Rate = 25,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_02,
                    Rate = 10,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_03,
                    Rate = 75,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_04,
                    Rate = 30,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_06,
                    Rate = 53,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_07,
                    Rate = 99,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_10,
                   Rate = 10,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                },
                new ProductDiscount
                {
                    ProductId = SeedProductIdConstants.Product_10,
                    Rate = 10,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(30),
                }
            };
            collection.InsertManyAsync(inventoryToAdd);
        }
    }

}
