using TaxiApp.DAL.Abstractions.Repositories;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Abstractions.Models;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Domain.Services
{
    public sealed class DriversService : IDriversService
    {
        private readonly IDriversRepository _driversRepository;

        public DriversService(IDriversRepository driversRepository)
        {
            _driversRepository = driversRepository;
        }

        public IAsyncEnumerable<DriverInfo> GetAll(bool filterActive)
        {
            var enumerable = filterActive ?
                _driversRepository.GetAllByState(DriverState.Active) :
                _driversRepository.GetAll();

            return enumerable.Select(x => new DriverInfo(
                x.Id,
                x.LastName,
                x.FirstName,
                x.Patronymic,
                x.State,
                x.TariffName
            ));
        }
    }
}
