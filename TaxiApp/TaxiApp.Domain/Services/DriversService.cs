using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Abstractions;
using TaxiApp.DAL.Abstractions.Entities;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Domain.Services
{
    public sealed class DriversService : IDriversService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DriversService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IAsyncEnumerable<DriverInfo> GetAll(bool filterActive)
        {
            var queryable = (IQueryable<DriverEntity>)_applicationDbContext.Drivers;

            if (filterActive)
                queryable = queryable.Where(x => x.State == DriverState.Active);

            return queryable
                .Select(x => new DriverInfo(
                    x.Id,
                    new FullName(
                        x.LastName,
                        x.FirstName,
                        x.Patronymic
                    ),
                    x.State,
                    x.Tariff.Name
                ))
                .AsAsyncEnumerable();
        }
    }
}
