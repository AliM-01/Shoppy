using _0_Framework.Infrastructure.Helpers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OM.Domain.Order;
using OM.Infrastructure.Persistence.Settings;

namespace OM.Infrastructure.Configuration;

public static class OrderModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<OrderDbSettings>(config.GetSection("OrderDbSettings"));

        services.AddScoped<IRepository<Order>, BaseRepository<Order, OrderDbSettings>>();
        services.AddScoped<IRepository<OrderItem>, BaseRepository<OrderItem, OrderDbSettings>>();

        services.AddMediatR(typeof(OrderModuletBootstrapper).Assembly);
    }
}
