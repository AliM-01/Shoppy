using _0_Framework.Infrastructure.Helpers;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
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

        services.AddScoped<IMongoHelper<ArticleCategory>, MongoHelper<ArticleCategory, BlogDbSettings>>();
        services.AddScoped<IMongoHelper<Article>, MongoHelper<Article, BlogDbSettings>>();

        services.AddMediatR(typeof(BlogModuletBootstrapper).Assembly);
    }
}

