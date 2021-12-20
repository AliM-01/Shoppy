using IM.Domain.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IM.Infrastructure.Persistence.Mappings;
public class InventoryMapping : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventory");
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Operations)
            .WithOne(x => x.Inventory)
            .HasForeignKey(x => x.InventoryId);
    }
}