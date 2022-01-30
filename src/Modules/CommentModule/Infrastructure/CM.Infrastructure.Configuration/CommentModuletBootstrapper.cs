using _0_Framework.Domain.IGenericRepository;
using _0_Framework.Infrastructure.GenericRepository;
using _01_Shoppy.Query.Contracts.Comment;
using _01_Shoppy.Query.Query.Comment;
using CM.Domain.Comment;
using CM.Infrastructure.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CM.Infrastructure.Configuration;

public static class CommentModuletBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddScoped<IGenericRepository<Comment>, GenericRepository<CommentDbContext, Comment>>();

        services.AddScoped<ICommentQuery, CommentQuery>();

        services.AddMediatR(typeof(CommentModuletBootstrapper).Assembly);

        services.AddDbContext<CommentDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}