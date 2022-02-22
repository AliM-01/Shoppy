using DM.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Infrastructure.Configuration;

public static class DiscountModuleBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddMediatR(typeof(DiscountModuleBootstrapper).Assembly);

        services.AddDbContext<DiscountDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}