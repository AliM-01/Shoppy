using _0_Framework.Infrastructure.Context;
using DM.Domain.ColleagueDiscount;
using DM.Domain.ProductDiscount;
using Microsoft.EntityFrameworkCore;

namespace DM.Infrastructure.Persistence.Context;
public class DiscountDbContext : DbContext
{
    #region Ctor

    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options) { }

    #endregion

    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }


    #region On Model Creating

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplySharedDbContextOnModelCreatingConfiguration();

        var assembly = typeof(DiscountDbContext).Assembly;
        builder.ApplyConfigurationsFromAssembly(assembly);

        //  Is Discount Expired Query Filter
        builder.Entity<ProductDiscount>().HasQueryFilter(b =>
                EF.Property<DateTime>(b, "StartDate") < DateTime.Now || EF.Property<DateTime>(b, "EndDate") >= DateTime.Now);

        base.OnModelCreating(builder);
    }

    #endregion On Model Creating
}