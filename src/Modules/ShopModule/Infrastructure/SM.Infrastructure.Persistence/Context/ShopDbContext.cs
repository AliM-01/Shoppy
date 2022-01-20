using _0_Framework.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using SM.Domain.Product;
using SM.Domain.ProductCategory;
using SM.Domain.ProductFeature;
using SM.Domain.ProductPicture;
using SM.Domain.Slider;

namespace SM.Infrastructure.Persistence.Context;
public class ShopDbContext : DbContext
{
    #region Ctor

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

    #endregion

    public DbSet<ProductCategory> ProductCategories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductPicture> ProductPictures { get; set; }
    public DbSet<ProductFeature> ProductFeatures { get; set; }

    public DbSet<Slider> Sliders { get; set; }

    #region On Model Creating

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplySharedDbContextOnModelCreatingConfiguration();

        var assembly = typeof(ShopDbContext).Assembly;
        builder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(builder);
    }

    #endregion On Model Creating
}