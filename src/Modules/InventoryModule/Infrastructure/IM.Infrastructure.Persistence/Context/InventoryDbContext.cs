using _0_Framework.Infrastructure.Context;
using IM.Domain.Inventory;
using Microsoft.EntityFrameworkCore;

namespace IM.Infrastructure.Persistence.Context;
public class InventoryDbContext : DbContext
{
    #region Ctor

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

    #endregion

    public DbSet<Inventory> Inventory { get; set; }
    public DbSet<InventoryOperation> InventoryOperations { get; set; }

    #region On Model Creating

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplySharedDbContextOnModelCreatingConfiguration();

        var assembly = typeof(InventoryDbContext).Assembly;
        builder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(builder);
    }

    #endregion On Model Creating
}