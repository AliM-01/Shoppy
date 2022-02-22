using _0_Framework.Domain.IGenericRepository;
using _0_Framework.Infrastructure.GenericRepository;
using DM.Domain.ColleagueDiscount;
using DM.Domain.ProductDiscount;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DM.Infrastructure.Persistence.Context;

namespace DM.Infrastructure.Configuration;

public static class DiscountModuleBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IGenericRepository<ProductDiscount>, GenericRepository<DiscountDbContext, ProductDiscount>>();
        services.AddScoped<IGenericRepository<ColleagueDiscount>, GenericRepository<DiscountDbContext, ColleagueDiscount>>();

        services.AddMediatR(typeof(DiscountModuleBootstrapper).Assembly);

        services.AddDbContext<DiscountDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}