using _0_Framework.Domain.IGenericRepository;
using _0_Framework.Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Contracts.ProductCategory.Interfaces;
using SM.Application.ProductCategory;
using SM.Domain.ProductCategory;
using SM.Infrastructure.Persistence.Context;

namespace SM.Configuration
{
    public static class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ShopDbContext, ProductCategory>>();
            services.AddScoped<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}