using _0_Framework.Domain.IGenericRepository;
using _0_Framework.Infrastructure.GenericRepository;
using DM.Domain.ColleagueDiscount;
using DM.Domain.CustomerDiscount;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Infrastructure.Persistence.Context;

namespace DM.Infrastructure.Configuration;

public static class DiscountModuleBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IGenericRepository<CustomerDiscount>, GenericRepository<DiscountDbContext, CustomerDiscount>>();
        services.AddScoped<IGenericRepository<ColleagueDiscount>, GenericRepository<DiscountDbContext, ColleagueDiscount>>();

        services.AddMediatR(typeof(DiscountModuleBootstrapper).Assembly);

        services.AddDbContext<DiscountDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}