﻿using _0_Framework.Infrastructure.Helpers;
using BM.Domain.Article;
using BM.Domain.ArticleCategory;
using BM.Infrastructure.Persistence.Seed;
using BM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BM.Infrastructure.Configuration;

public class BlogModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<BlogDbSettings>(config.GetSection("BlogDbSettings"));

        services.AddScoped<IGenericRepository<ArticleCategory>, GenericRepository<ArticleCategory, BlogDbSettings>>();
        services.AddScoped<IGenericRepository<Article>, GenericRepository<Article, BlogDbSettings>>();

        services.AddMediatR(typeof(BlogModuletBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            try
            {
                var dbSettings = (BlogDbSettings)config.GetSection("BlogDbSettings").Get(typeof(BlogDbSettings));

                var categories = BlogDbDataSeed.SeedArticleCategoryData(dbSettings);
                BlogDbDataSeed.SeedArticleData(dbSettings, categories);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

