using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Helpers.Product;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SM.Domain.Product;
using SM.Domain.ProductCategory;
using SM.Domain.ProductFeature;
using SM.Domain.ProductPicture;
using SM.Domain.Slider;
using SM.Infrastructure.Persistence.Context;
using SM.Infrastructure.Persistence.Seeds;
using SM.Infrastructure.Persistence.Settings;
using System;

namespace SM.Infrastructure.Configuration;

public static class ShopModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.AddScoped<IGenericRepository<Product>, GenericRepository<Product, ShopDbSettings>>();

        services.Configure<ShopDbSettings>(config.GetSection("ShopDbSettings"));

        services.AddScoped<IShopDbContext, ShopDbContext>();

        services.AddTransient<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory, ShopDbSettings>>();
        services.AddTransient<IGenericRepository<ProductPicture>, GenericRepository<ProductPicture, ShopDbSettings>>();
        services.AddTransient<IGenericRepository<ProductFeature>, GenericRepository<ProductFeature, ShopDbSettings>>();
        services.AddTransient<IGenericRepository<Slider>, GenericRepository<Slider, ShopDbSettings>>();

        services.AddScoped<IProductHelper, ProductHelper>();

        services.AddMediatR(typeof(ShopModuletBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            try
            {
                var shopContext = scope.ServiceProvider.GetRequiredService<IShopDbContext>();
                var categories = ShopDbSeed.SeedProductCategories(shopContext.ProductCategories);
                ShopDbSeed.SeedProducts(shopContext.Products, categories);
                ShopDbSeed.SeedProductPictures(shopContext.ProductPictures);
                ShopDbSeed.SeedProductFeatures(shopContext.ProductFeatures);
                ShopDbSeed.SeedSliders(shopContext.Sliders);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}