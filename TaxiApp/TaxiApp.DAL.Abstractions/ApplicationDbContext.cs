using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Entities;

namespace TaxiApp.DAL
{
    public interface IApplicationDbContext
    {
        DbSet<CarEntity> Cars { get; }
        DbSet<CarTariffEntity> CarTariffs { get; }
        DbSet<ClientEntity> Clients { get; }
        DbSet<DriverEntity> Drivers { get; }
        DbSet<OrderEntity> Orders { get; }
        DbSet<TariffEntity> Tariffs { get; }
        DbSet<UserEntity> Users { get; }

        Task Migrate();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public abstract class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<CarTariffEntity> CarTariffs { get; set; }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<DriverEntity> Drivers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<TariffEntity> Tariffs { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public abstract Task Migrate();
    }
}
