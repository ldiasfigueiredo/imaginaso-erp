using ImaginaSo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImaginaSo.Infrastructure.Data.Configurations
{
    public class PrinterConfiguration : IEntityTypeConfiguration<Printer>
    {
        public void Configure(EntityTypeBuilder<Printer> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(120);
            builder.Property(p => p.Model)
                .HasMaxLength(120);
            builder.Property(p => p.Manufacturer)
                .HasMaxLength(120);

            builder.Property(p => p.PowerConsumptionWatts).HasPrecision(10, 2);
            builder.Property(p => p.PurchasePrice).HasPrecision(12, 2);
            builder.Property(p => p.InvestmentReturnYears).HasPrecision(6, 2);
            builder.Property(p => p.HoursWorkedPerYear).HasPrecision(8, 2);
            builder.Property(p => p.DepreciationCostPerHour).HasPrecision(18, 6);
        }
    }
}
