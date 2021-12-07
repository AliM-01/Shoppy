using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.Slider;

namespace SM.Infrastructure.Persistence.Mappings;
public class SliderMapping : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.ToTable("Sliders");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Heading).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Text).HasMaxLength(250).IsRequired();
        builder.Property(x => x.ImagePath).IsRequired();
        builder.Property(x => x.ImageAlt).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ImageTitle).HasMaxLength(100).IsRequired();
        builder.Property(x => x.BtnLink).IsRequired();
        builder.Property(x => x.BtnText).HasMaxLength(50).IsRequired();
    }
}