using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Abstractions.Entities;

namespace TaxiApp.DAL.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<CarEntity> Cars { get; }
        DbSet<ClientEntity> Clients { get; }
        DbSet<DriverEntity> Drivers { get; }
        DbSet<OrderEntity> Orders { get; }
        DbSet<TariffEntity> Tariffs { get; }
        DbSet<UserEntity> Users { get; }

        Task Migrate();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
