using ImaginaSo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImaginaSo.Infrastructure.Data.Configurations
{
    public class SalesChannelConfiguration : IEntityTypeConfiguration<SalesChannel>
    {
        public void Configure(EntityTypeBuilder<SalesChannel> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(80);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.Property(c => c.FeePercentage)
                .HasPrecision(5, 2);
        }
    }
}
