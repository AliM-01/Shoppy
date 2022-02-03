using _0_Framework.Domain.IGenericRepository;
using _0_Framework.Infrastructure.GenericRepository;
using BM.Domain.ArticleCategory;
using BM.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BM.Infrastructure.Configuration;

public class BlogModuletBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IGenericRepository<ArticleCategory>, GenericRepository<BlogDbContext, ArticleCategory>>();

        services.AddMediatR(typeof(BlogModuletBootstrapper).Assembly);

        services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}

