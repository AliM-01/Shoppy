using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductPicture;

namespace SM.Infrastructure.Persistence.Mappings;
public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
{
    public void Configure(EntityTypeBuilder<ProductPicture> builder)
    {
        builder.ToTable("ProductPictures");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImagePath).IsRequired();
        builder.Property(x => x.ImageAlt).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ImageTitle).HasMaxLength(100).IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductPictures)
            .HasForeignKey(x => x.ProductId);
    }
}