using BM.Domain.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BM.Infrastructure.Persistence.Mappings;
public class ArticleMapping : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Articles");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Summary).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Text).IsRequired();
        builder.Property(x => x.ImagePath).IsRequired();
        builder.Property(x => x.ImageAlt).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ImageTitle).HasMaxLength(100).IsRequired();
        builder.Property(x => x.MetaKeywords).HasMaxLength(80).IsRequired();
        builder.Property(x => x.MetaDescription).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Slug).IsRequired();
    }
}