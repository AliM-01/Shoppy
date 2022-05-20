using _0_Framework.Infrastructure.IRepository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OM.Domain.Order;
using OM.Infrastructure.Persistence.Settings;

namespace OM.Infrastructure.Configuration;

public static class OrderModuleBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<OrderDbSettings>(config.GetSection("OrderDbSettings"));

        services.AddTransient<IRepository<Order>, BaseRepository<Order, OrderDbSettings>>();
        services.AddTransient<IRepository<OrderItem>, BaseRepository<OrderItem, OrderDbSettings>>();

        services.AddMediatR(typeof(OrderModuleBootstrapper).Assembly);
    }
}
