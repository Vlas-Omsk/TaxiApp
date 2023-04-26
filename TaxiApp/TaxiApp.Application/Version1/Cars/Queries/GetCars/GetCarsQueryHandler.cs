using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1.DTO;
using TaxiApp.Application.Version1.Queries;
using TaxiApp.Domain.Abstractions.Services;

namespace TaxiApp.Application.Version1.Cars.Queries.GetCars
{
    internal sealed class GetCarsQueryHandler : RequestHandler<GetCarsQuery, CarDTO[]>
    {
        private readonly ICarsService _carsService;

        public GetCarsQueryHandler(ICarsService carsService)
        {
            _carsService = carsService;
        }

        protected override async Task<Response<CarDTO[]>> ExecuteOverride(GetCarsQuery request)
        {
            var result = await _carsService.GetAllWithDriverFullName()
                .Select(x => new CarDTO(
                    x.Id,
                    x.Brand,
                    x.Number,
                    x.Color,
                    x.DriverFullName
                ))
                .ToArrayAsync();

            return Success(result);
        }
    }
}
