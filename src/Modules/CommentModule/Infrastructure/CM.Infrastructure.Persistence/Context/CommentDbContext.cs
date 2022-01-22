using _0_Framework.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IM.Infrastructure.Persistence.Context;
public class CommentDbContext : DbContext
{
    #region Ctor

    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

    #endregion

    public DbSet<CM.Domain.Comment.Comment> Comments { get; set; }

    #region On Model Creating

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplySharedDbContextOnModelCreatingConfiguration();

        var assembly = typeof(CommentDbContext).Assembly;
        builder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(builder);
    }

    #endregion On Model Creating
}