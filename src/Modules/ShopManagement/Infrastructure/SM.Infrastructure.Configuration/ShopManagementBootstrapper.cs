using _0_Framework.Domain.IGenericRepository;
using _0_Framework.Infrastructure.GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Contracts.ProductCategory.Queries;
using SM.Application.ProductCategory.QueryHandles;
using SM.Domain.ProductCategory;
using SM.Infrastructure.Persistence.Context;

namespace SM.Infrastructure.Configuration
{
    public static class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ShopDbContext, ProductCategory>>();

            services.AddMediatR(typeof(ShopManagementBootstrapper).Assembly);

            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}