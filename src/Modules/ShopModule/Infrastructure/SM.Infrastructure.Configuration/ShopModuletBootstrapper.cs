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
using SM.Domain.ProductPicture;
using SM.Domain.Slider;
using SM.Infrastructure.Persistence.Context;

namespace SM.Infrastructure.Configuration;

public static class ShopModuletBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ShopDbContext, ProductCategory>>();
        services.AddScoped<IGenericRepository<Product>, GenericRepository<ShopDbContext, Product>>();
        services.AddScoped<IGenericRepository<ProductPicture>, GenericRepository<ShopDbContext, ProductPicture>>();
        services.AddScoped<IGenericRepository<Slider>, GenericRepository<ShopDbContext, Slider>>();

        services.AddMediatR(typeof(ShopModuletBootstrapper).Assembly);

        services.AddScoped<IProductHelper, ProductHelper>();

        services.AddScoped<ISliderQuery, SliderQuery>();
        services.AddScoped<IProductCategoryQuery, ProductCategoryQuery>();
        services.AddTransient<IProductQuery, ProductQuery>();

        services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}