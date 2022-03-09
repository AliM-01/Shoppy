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

        services.AddScoped<IGenericRepository<Order>, GenericRepository<Order, OrderDbSettings>>();
        services.AddScoped<IGenericRepository<OrderItem>, GenericRepository<OrderItem, OrderDbSettings>>();

        services.AddMediatR(typeof(OrderModuletBootstrapper).Assembly);
    }
}
