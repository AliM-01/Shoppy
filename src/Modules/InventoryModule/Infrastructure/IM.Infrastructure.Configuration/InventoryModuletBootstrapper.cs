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
        services.Configure<InventoryDbSettings>(config.GetSection("InventoryDbSettings"));

        services.AddScoped<IInventoryDbContext, InventoryDbContext>();

        services.AddScoped<IGenericRepository<Inventory>, GenericRepository<Inventory, InventoryDbSettings>>();
        services.AddScoped<IGenericRepository<InventoryOperation>, GenericRepository<InventoryOperation, InventoryDbSettings>>();

        services.AddScoped<IInventoryHelper, InventoryHelper>();

        services.AddMediatR(typeof(InventoryModuletBootstrapper).Assembly);
    }
}