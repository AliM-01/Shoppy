using _0_Framework.Infrastructure.IRepository;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using BM.Infrastructure.Persistence.Seed;
using BM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BM.Infrastructure.Configuration;

public class BlogModuleBootstrapper
{
    public static void Configure(IServiceCollection services, IConfiguration config)
    {
        services.Configure<BlogDbSettings>(config.GetSection("BlogDbSettings"));

        services.AddScoped<IRepository<ArticleCategory>, BaseRepository<ArticleCategory, BlogDbSettings>>();
        services.AddScoped<IRepository<Article>, BaseRepository<Article, BlogDbSettings>>();

        services.AddMediatR(typeof(BlogModuleBootstrapper).Assembly);

        #region Db Seed

        using var scope = services.BuildServiceProvider().CreateScope();
        var sp = scope.ServiceProvider;
        var logger = sp.GetRequiredService<ILogger<BlogModuleBootstrapper>>();

        try
        {
            var dbSettings = (BlogDbSettings)config.GetSection("BlogDbSettings").Get(typeof(BlogDbSettings));

            var categories = BlogDbDataSeed.SeedArticleCategoryData(dbSettings);
            BlogDbDataSeed.SeedArticleData(dbSettings, categories);

            logger.LogInformation("Blog Module Db Seed Finished Successfully");
        }
        catch (Exception ex)
        {
            logger.LogError("Blog Module Db Seed Was Unsuccessfull. Execption : {0}", ex.Message);
        }

        #endregion
    }
}

