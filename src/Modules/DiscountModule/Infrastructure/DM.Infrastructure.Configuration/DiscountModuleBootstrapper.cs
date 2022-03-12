using _0_Framework.Infrastructure.Helpers;
using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;
using DM.Infrastructure.Persistence.Seeds;
using DM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Infrastructure.Configuration;

public static class DiscountModuleBootstrapper
{
    public static void Configure(IServiceCollection services, IConfiguration config)
    {
        services.Configure<DiscountDbSettings>(config.GetSection("DiscountDbSettings"));

        services.AddScoped<IGenericRepository<DiscountCode>, GenericRepository<DiscountCode, DiscountDbSettings>>();
        services.AddScoped<IGenericRepository<ProductDiscount>, GenericRepository<ProductDiscount, DiscountDbSettings>>();

        services.AddMediatR(typeof(DiscountModuleBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            try
            {
                var dbSettings = (DiscountDbSettings)config.GetSection("DiscountDbSettings").Get(typeof(DiscountDbSettings));

                DiscountDbSeed.SeedDiscountCodes(dbSettings);
                DiscountDbSeed.SeedProductDiscounts(dbSettings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}