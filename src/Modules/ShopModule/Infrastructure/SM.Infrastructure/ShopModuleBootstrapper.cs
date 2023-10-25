using _0_Framework.Infrastructure.IRepository;
using _01_Shoppy.Query.Helpers.Product;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SM.Domain.Product;
using SM.Domain.ProductCategory;
using SM.Domain.ProductFeature;
using SM.Domain.ProductPicture;
using SM.Domain.Slider;
using SM.Infrastructure;
using SM.Infrastructure;
using System;

namespace SM.Infrastructure;

public class ShopModuleBootstrapper
{
    public static void Configure(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IRepository<Product>, BaseRepository<Product, ShopDbSettings>>();

        services.Configure<ShopDbSettings>(config.GetSection("ShopDbSettings"));

        services.AddTransient<IRepository<ProductCategory>, BaseRepository<ProductCategory, ShopDbSettings>>();
        services.AddTransient<IRepository<ProductPicture>, BaseRepository<ProductPicture, ShopDbSettings>>();
        services.AddTransient<IRepository<ProductFeature>, BaseRepository<ProductFeature, ShopDbSettings>>();
        services.AddTransient<IRepository<Slider>, BaseRepository<Slider, ShopDbSettings>>();

        services.AddTransient<IProductHelper, ProductHelper>();

        services.AddMediatR(typeof(ShopModuleBootstrapper).Assembly);

        #region Db Seed

        using var scope = services.BuildServiceProvider().CreateScope();
        var sp = scope.ServiceProvider;

        var logger = sp.GetRequiredService<ILogger<ShopModuleBootstrapper>>();

        try
        {
            var dbSettings = (ShopDbSettings)config.GetSection("ShopDbSettings").Get(typeof(ShopDbSettings));

            var categories = ShopDbSeed.SeedProductCategories(dbSettings);
            ShopDbSeed.SeedProducts(dbSettings, categories);
            ShopDbSeed.SeedProductPictures(dbSettings);
            ShopDbSeed.SeedProductFeatures(dbSettings);
            ShopDbSeed.SeedSliders(dbSettings);

            logger.LogInformation("Shop Module Db Seed Finished Successfully");
        }
        catch (Exception ex)
        {
            logger.LogError("Shop Module Db Seed Was Unsuccessfull. Execption : {0}", ex.Message);
        }

        #endregion
    }
}