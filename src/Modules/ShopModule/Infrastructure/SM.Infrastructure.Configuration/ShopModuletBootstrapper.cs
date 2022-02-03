using _0_Framework.Domain.IGenericRepository;
using _0_Framework.Infrastructure.GenericRepository;
using _01_Shoppy.Query.Contracts.Product;
using _01_Shoppy.Query.Contracts.ProductCategory;
using _01_Shoppy.Query.Contracts.Slider;
using _01_Shoppy.Query.Helpers.Product;
using _01_Shoppy.Query.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Domain.Product;
using SM.Domain.ProductCategory;
using SM.Domain.ProductFeature;
using SM.Domain.ProductPicture;
using SM.Domain.Slider;
using SM.Infrastructure.Persistence.Context;

namespace SM.Infrastructure.Configuration;

public static class ShopModuletBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IGenericRepository<Product>, GenericRepository<ShopDbContext, Product>>();

        services.AddTransient<IGenericRepository<ProductCategory>, GenericRepository<ShopDbContext, ProductCategory>>();
        services.AddTransient<IGenericRepository<ProductPicture>, GenericRepository<ShopDbContext, ProductPicture>>();
        services.AddTransient<IGenericRepository<ProductFeature>, GenericRepository<ShopDbContext, ProductFeature>>();
        services.AddTransient<IGenericRepository<Slider>, GenericRepository<ShopDbContext, Slider>>();

        services.AddMediatR(typeof(ShopModuletBootstrapper).Assembly);

        services.AddScoped<IProductHelper, ProductHelper>();

        services.AddTransient<ISliderQuery, SliderQuery>();
        services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

        services.AddScoped<IProductQuery, ProductQuery>();

        services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}