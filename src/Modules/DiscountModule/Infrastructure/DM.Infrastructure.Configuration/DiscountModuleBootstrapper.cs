using _0_Framework.Infrastructure.Helpers;
using DM.Domain.ColleagueDiscount;
using DM.Domain.ProductDiscount;
using DM.Infrastructure.Persistence.Context;
using DM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DM.Infrastructure.Configuration;

public static class DiscountModuleBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<DiscountDbSettings>(config.GetSection("DiscountDbSettings"));

        services.AddScoped<IDiscountDbContext, DiscountDbContext>();

        services.AddScoped<IMongoHelper<ColleagueDiscount>, MongoHelper<ColleagueDiscount, DiscountDbSettings>>();
        services.AddScoped<IMongoHelper<ProductDiscount>, MongoHelper<ProductDiscount, DiscountDbSettings>>();

        services.AddMediatR(typeof(DiscountModuleBootstrapper).Assembly);
    }
}