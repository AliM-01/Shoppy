﻿using _0_Framework.Infrastructure.IRepository;
using CM.Application.Contracts.Sevices;
using CM.Domain.Comment;
using CM.Infrastructure.ArticleAcl;
using CM.Infrastructure.Persistence.Context;
using CM.Infrastructure.Persistence.Settings;
using CM.Infrastructure.ProductAcl;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace CM.Infrastructure.Configuration;

public class CommentModuleBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<CommentDbSettings>(config.GetSection("CommentDbSettings"));

        services.AddScoped<IRepository<Comment>, BaseRepository<Comment, CommentDbSettings>>();
        services.AddScoped<ICMProductAcl, CMProuctAclService>();
        services.AddScoped<ICMArticleAcl, CMArticleAclService>();

        services.AddMediatR(typeof(CommentModuleBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider())
        {
            var logger = scope.GetRequiredService<ILogger<CommentModuleBootstrapper>>();

            try
            {
                var dbSettings = (CommentDbSettings)config.GetSection("CommentDbSettings").Get(typeof(CommentDbSettings));

                CommentDbContextSeed.SeedData(dbSettings);

                logger.LogInformation("Comment Module Db Seed Finished Successfully");
            }
            catch (Exception ex)
            {
                logger.LogError("Comment Module Db Seed Was Unsuccessfull. Execption : {0}", ex.Message);
            }
        }
    }
}