using _0_Framework.Infrastructure.IRepository;
using DM.Application.Contracts.Sevices;
using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;
using DM.Infrastructure.Persistence.Seeds;
using DM.Infrastructure.Persistence.Settings;
using DM.Infrastructure.ProductAcl;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DM.Infrastructure.Configuration;

public class DiscountModuleBootstrapper
{
    public static void Configure(IServiceCollection services, IConfiguration config)
    {
        services.Configure<DiscountDbSettings>(config.GetSection("DiscountDbSettings"));

        services.AddScoped<IRepository<DiscountCode>, BaseRepository<DiscountCode, DiscountDbSettings>>();
        services.AddScoped<IRepository<ProductDiscount>, BaseRepository<ProductDiscount, DiscountDbSettings>>();
        services.AddScoped<IDMProucAclService, DMProucAclService>();

        services.AddMediatR(typeof(DiscountModuleBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider())
        {
            var logger = scope.GetRequiredService<ILogger<DiscountModuleBootstrapper>>();

            try
            {
                var dbSettings = (DiscountDbSettings)config.GetSection("DiscountDbSettings").Get(typeof(DiscountDbSettings));

                DiscountDbSeed.SeedDiscountCodes(dbSettings);
                DiscountDbSeed.SeedProductDiscounts(dbSettings);

                logger.LogInformation("Discount Module Db Seed Finished Successfully");
            }
            catch (Exception ex)
            {
                logger.LogError("Discount Module Db Seed Was Unsuccessfull. Execption : {0}", ex.Message);
            }
        }
    }
}