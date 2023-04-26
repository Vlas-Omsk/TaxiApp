using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Abstractions.Models;
using TaxiApp.DAL.Abstractions.Repositories;
using TaxiApp.DAL.Entities;
using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Repositories
{
    public sealed class DriversRepository : IDriversRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DriversRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IAsyncEnumerable<DriverInfoModel> GetAll()
        {
            return _applicationDbContext.Set<DriverEntity>()
                .Select(x => new DriverInfoModel(
                    x.Id,
                    x.LastName,
                    x.FirstName,
                    x.Patronymic,
                    x.State,
                    x.Tariff.Name
                ))
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<DriverInfoModel> GetAllByState(DriverState state)
        {
            return _applicationDbContext.Set<DriverEntity>()
                .Where(x => x.State == state)
                .Select(x => new DriverInfoModel(
                    x.Id,
                    x.LastName,
                    x.FirstName,
                    x.Patronymic,
                    x.State,
                    x.Tariff.Name
                ))
                .AsAsyncEnumerable();
        }
    }
}
