using _0_Framework.Infrastructure.Helpers;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using BM.Infrastructure.Persistence.Context;
using BM.Infrastructure.Persistence.Seed;
using BM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

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

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            try
            {
                var articleService = scope.ServiceProvider.GetRequiredService<IBlogDbContext>();

                var categories = BlogDbDataSeed.SeedArticleCategoryData(articleService.ArticleCategories);
                BlogDbDataSeed.SeedArticleData(articleService.Articles, categories);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

