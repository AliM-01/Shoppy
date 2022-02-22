using _0_Framework.Infrastructure.Helpers;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Application.Inventory.Helpers;
using IM.Domain.Inventory;
using IM.Infrastructure.Persistence.Context;
using IM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace IM.Infrastructure.Configuration;

public static class InventoryModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<inventoryRepositorySettings>(config.GetSection("inventoryRepositorySettings"));

        services.AddScoped<IinventoryRepositoryContext, inventoryRepositoryContext>();

        services.AddScoped<IGenericRepository<Inventory>, GenericRepository<Inventory, inventoryRepositorySettings>>();
        services.AddScoped<IGenericRepository<InventoryOperation>, GenericRepository<InventoryOperation, inventoryRepositorySettings>>();

        services.AddScoped<IInventoryHelper, InventoryHelper>();

        services.AddMediatR(typeof(InventoryModuletBootstrapper).Assembly);
    }
}