using _0_Framework.Infrastructure.Helpers;
using CM.Domain.Comment;
using CM.Infrastructure.Persistence.Context;
using CM.Infrastructure.Persistence.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CM.Infrastructure.Configuration;

public static class CommentModuletBootstrapper
{
    public static void Configure(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration config)
    {
        services.Configure<CommentDbSettings>(config.GetSection("CommentDbSettings"));

        services.AddScoped<ICommentDbContext, CommentDbContext>();

        services.AddScoped<IMongoHelper<Comment>, MongoHelper<Comment, CommentDbSettings>>();

        services.AddMediatR(typeof(CommentModuletBootstrapper).Assembly);
    }
}