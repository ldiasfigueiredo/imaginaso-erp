using ImaginaSo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImaginaSo.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Printer> Printers => Set<Printer>();
        public DbSet<Filament> Filaments => Set<Filament>();
        public DbSet<GlobalSettings> Settings => Set<GlobalSettings>();
        public DbSet<ExtraCostItem> ExtraCostItems => Set<ExtraCostItem>();
        public DbSet<SalesChannel> SalesChannels => Set<SalesChannel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Carrega automaticamente todas as classes IEntityTypeConfiguration<T> deste projeto
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
