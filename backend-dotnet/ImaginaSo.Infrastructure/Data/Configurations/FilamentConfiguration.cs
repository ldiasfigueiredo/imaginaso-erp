using ImaginaSo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImaginaSo.Infrastructure.Data.Configurations
{
    public class FilamentConfiguration : IEntityTypeConfiguration<Filament>
    {
        public void Configure(EntityTypeBuilder<Filament> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedNever();

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(f => f.Manufacturer)
                .HasMaxLength(120);
            builder.Property(f => f.MaterialType)
                .HasMaxLength(50);
            builder.Property(f => f.Color)
                .HasMaxLength(60);
            builder.Property(f => f.ColorHex)
                .HasMaxLength(7);

            builder.Property(f => f.PricePerKg).HasPrecision(10, 2);
         }
    }
}
