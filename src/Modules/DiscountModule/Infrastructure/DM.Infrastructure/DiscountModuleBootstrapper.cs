using _0_Framework.Infrastructure.IRepository;
using DM.Application.Sevices;
using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;
using DM.Domain.Settings;
using DM.Infrastructure.AclServices;
using DM.Infrastructure.Persistence.Seeds;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DM.Infrastructure;

public class DiscountModuleBootstrapper
{
    public static void Configure(IServiceCollection services, IConfiguration config)
    {
        services.Configure<DiscountDbSettings>(config.GetSection("DiscountDbSettings"));

        services.AddTransient<IRepository<DiscountCode>, BaseRepository<DiscountCode, DiscountDbSettings>>();
        services.AddTransient<IRepository<ProductDiscount>, BaseRepository<ProductDiscount, DiscountDbSettings>>();
        services.AddTransient<IDMProucAclService, DMProucAclService>();

        services.AddMediatR(typeof(DiscountModuleBootstrapper).Assembly);

        #region Db Seed

        using var scope = services.BuildServiceProvider().CreateScope();
        var sp = scope.ServiceProvider;

        var logger = sp.GetRequiredService<ILogger<DiscountModuleBootstrapper>>();

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

        #endregion
    }
}