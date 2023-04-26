using TaxiApp.DAL.Abstractions.Repositories;
using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Domain.Services
{
    internal sealed class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsRepository;

        public CarsService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public IAsyncEnumerable<CarInfo> GetAllWithDriverFullName()
        {
            return _carsRepository.GetAllWithDriverFullName()
                .Select(x => new CarInfo(
                    x.Id,
                    x.Brand,
                    x.Number,
                    x.Color,
                    x.DriverFullName
                ));
        }
    }
}
