using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.Product;

namespace SM.Infrastructure.Persistence.Mappings;
public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Code).HasMaxLength(15).IsRequired();
        builder.Property(x => x.ShortDescription).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(2500).IsRequired();
        builder.Property(x => x.ImagePath).IsRequired();
        builder.Property(x => x.ImageAlt).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ImageTitle).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MetaKeywords).HasMaxLength(80).IsRequired();
        builder.Property(x => x.MetaDescription).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Slug).IsRequired();

        builder.HasMany(x => x.ProductPictures)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);

        builder.Navigation(p => p.Category).AutoInclude();
    }
}