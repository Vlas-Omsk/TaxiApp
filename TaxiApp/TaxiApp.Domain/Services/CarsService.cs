using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Abstractions;
using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Domain.Services
{
    internal sealed class CarsService : ICarsService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CarsService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IAsyncEnumerable<CarInfo> GetAllWithDriverFullName()
        {
            return _applicationDbContext.Cars
                .Select(x => new CarInfo(
                    x.Id,
                    x.Brand,
                    x.Number,
                    x.Color,
                    x.Drivers
                        .Select(c => new FullName(
                            c.LastName,
                            c.FirstName,
                            c.Patronymic
                        ))
                        .ToArray()
                ))
                .AsAsyncEnumerable();
        }
    }
}
