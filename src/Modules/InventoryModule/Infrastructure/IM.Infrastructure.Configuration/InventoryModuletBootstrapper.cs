using _0_Framework.Infrastructure.IRepository;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Application.Contracts.Sevices;
using IM.Application.Inventory.Helpers;
using IM.Domain.Inventory;
using IM.Infrastructure.AccountAcl;
using IM.Infrastructure.Persistence.Seeds;
using IM.Infrastructure.Persistence.Settings;
using IM.Infrastructure.ProductAcl;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IM.Infrastructure.Configuration;

public static class InventoryModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<InventoryDbSettings>(config.GetSection("InventoryDbSettings"));

        services.AddScoped<IRepository<Inventory>, BaseRepository<Inventory, InventoryDbSettings>>();
        services.AddScoped<IRepository<InventoryOperation>, BaseRepository<InventoryOperation, InventoryDbSettings>>();

        services.AddScoped<IInventoryHelper, InventoryHelper>();
        services.AddScoped<IIMProuctAclService, IMProuctAclService>();
        services.AddScoped<IIMAccountAclService, IMAccountAclService>();

        services.AddMediatR(typeof(InventoryModuletBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            try
            {
                var dbSettings = (InventoryDbSettings)config.GetSection("InventoryDbSettings").Get(typeof(InventoryDbSettings));

                var inventories = InventoryDbSeed.SeedInventories(dbSettings);
                InventoryDbSeed.SeedInventoryOperations(dbSettings, inventories);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}