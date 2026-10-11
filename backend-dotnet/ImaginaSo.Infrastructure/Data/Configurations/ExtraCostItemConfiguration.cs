using ImaginaSo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImaginaSo.Infrastructure.Data.Configurations
{
    public class ExtraCostItemConfiguration : IEntityTypeConfiguration<ExtraCostItem>
    {
        public void Configure(EntityTypeBuilder<ExtraCostItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
            builder.HasIndex(e => e.Name).IsUnique();
            
            builder.Property(e => e.UnitCost)
                .HasPrecision(10, 4);
        }
    }
}
