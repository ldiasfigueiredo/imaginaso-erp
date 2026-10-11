using ImaginaSo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImaginaSo.Infrastructure.Data.Configurations
{
    public class GlobalSettingsConfiguration : IEntityTypeConfiguration<GlobalSettings>
    {
        public void Configure(EntityTypeBuilder<GlobalSettings> builder)
        {
            builder.ToTable("GlobalSettings");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedNever();

            builder.Property(s => s.ElectricityCostPerKWh).HasPrecision(10, 4);
            builder.Property(s => s.MaintenanceReservePercentage).HasPrecision(5, 2);
        }
    }
}
