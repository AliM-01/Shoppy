using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductFeature;

namespace SM.Infrastructure.Persistence.Mappings;

public class ProductFeatureMapping : IEntityTypeConfiguration<ProductFeature>
{
    public void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        builder.ToTable("ProductFeatures");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FeatureTitle).HasMaxLength(100).IsRequired();
        builder.Property(x => x.FeatureValue).HasMaxLength(250).IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductFeatures)
            .HasForeignKey(x => x.ProductId);
    }
}
