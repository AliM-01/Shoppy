using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductCategory;

namespace SM.Infrastructure.Persistence.Mappings
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(250).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.ImageAlt).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ImageTitle).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MetaKeywords).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Slug).IsRequired();
        }
    }
}