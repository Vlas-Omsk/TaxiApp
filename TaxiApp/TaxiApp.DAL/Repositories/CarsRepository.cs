using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Abstractions.Models;
using TaxiApp.DAL.Abstractions.Repositories;
using TaxiApp.DAL.Entities;

namespace TaxiApp.DAL.Repositories
{
    public sealed class CarsRepository : ICarsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CarsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IAsyncEnumerable<CarInfoWithDriverFullNameModel> GetAllWithDriverFullName()
        {
            return _applicationDbContext.Set<CarEntity>()
                .Select(x => new CarInfoWithDriverFullNameModel(
                    x.Id,
                    x.Brand,
                    x.Number,
                    x.Color,
                    string.Join(", ", x.Drivers.Select(x => $"{x.LastName} {x.FirstName} {x.Patronymic}"))
                ))
                .AsAsyncEnumerable();
        }
    }
}
