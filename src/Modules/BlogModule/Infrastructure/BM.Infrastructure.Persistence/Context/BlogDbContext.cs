using _0_Framework.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BM.Infrastructure.Persistence.Context;
public class BlogDbContext : DbContext
{
    #region Ctor

    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

    #endregion

    public DbSet<BM.Domain.ArticleCategory.ArticleCategory> ArticleCategories { get; set; }

    #region On Model Creating

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplySharedDbContextOnModelCreatingConfiguration();

        var assembly = typeof(BlogDbContext).Assembly;
        builder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(builder);
    }

    #endregion On Model Creating
}