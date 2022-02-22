using _0_Framework.Infrastructure.Helpers;
using CM.Domain.Comment;
using CM.Infrastructure.Persistence.Context;
using CM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CM.Infrastructure.Configuration;

public static class CommentModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<CommentDbSettings>(config.GetSection("CommentDbSettings"));

        services.AddScoped<ICommentDbContext, CommentDbContext>();

        services.AddScoped<IGenericRepository<Comment>, GenericRepository<Comment, CommentDbSettings>>();

        services.AddMediatR(typeof(CommentModuletBootstrapper).Assembly);

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            try
            {
                var commentService = scope.ServiceProvider.GetRequiredService<ICommentDbContext>();
                CommentDbContextSeed.SeedData(commentService.Comments);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}