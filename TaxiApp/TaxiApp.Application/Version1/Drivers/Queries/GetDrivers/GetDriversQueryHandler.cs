using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1.DTO;
using TaxiApp.Application.Version1.Queries;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Application.Version1.Drivers.Queries.GetDrivers
{
    internal sealed class GetDriversQueryHandler : RequestHandler<GetDriversQuery, DriverDTO[]>
    {
        private readonly IDriversService _driversService;

        public GetDriversQueryHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }

        protected override async Task<Response<DriverDTO[]>> ExecuteOverride(GetDriversQuery request)
        {
            return Success(
                await _driversService.GetAll(request.FilterActive)
                    .Select(x => new DriverDTO(
                        x.Id,
                        x.LastName,
                        x.FirstName,
                        x.Patronymic,
                        x.State,
                        x.TariffName
                    ))
                    .ToArrayAsync()
            );
        }
    }
}
