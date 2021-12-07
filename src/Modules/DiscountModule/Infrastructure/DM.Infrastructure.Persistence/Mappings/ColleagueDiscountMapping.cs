using DM.Domain.ColleagueDiscount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DM.Infrastructure.Persistence.Mappings;
public class ColleagueDiscountMapping : IEntityTypeConfiguration<ColleagueDiscount>
{
    public void Configure(EntityTypeBuilder<ColleagueDiscount> builder)
    {
        builder.ToTable("ColleagueDiscounts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Rate).IsRequired();
    }
}