using BM.Infrastructure.Persistence.Context;
using BM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BM.Infrastructure.Configuration;

public class BlogModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<BlogDbSettings>(config.GetSection("BlogDbSettings"));

        services.AddScoped<IBlogDbContext, BlogDbContext>();

        services.AddMediatR(typeof(BlogModuletBootstrapper).Assembly);
    }
}

