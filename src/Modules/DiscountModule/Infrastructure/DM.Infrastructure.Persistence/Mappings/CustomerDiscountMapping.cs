using DM.Domain.CustomerDiscount;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DM.Infrastructure.Persistence.Mappings;
public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
{
    public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
    {
        builder.ToTable("CustomerDiscounts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Rate).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(250).IsRequired();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
    }
}