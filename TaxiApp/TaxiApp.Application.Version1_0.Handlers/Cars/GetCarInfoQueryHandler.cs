using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Cars
{
    internal sealed class GetCarInfoQueryHandler : RequestHandler<GetCarInfoQuery, CarInfoDTO>
    {
        private readonly CarsService _carsService;

        public GetCarInfoQueryHandler(
            CarsService carsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _carsService = carsService;
        }

        protected override async Task<Response<CarInfoDTO>> ExecuteOverride(GetCarInfoQuery request)
        {
            var car = await _carsService.GetAll()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (car == null)
                return Fail(Errors.Cars.NotFound);

            return Success(new CarInfoDTO(
                car.Id,
                car.Number,
                car.Color,
                await _carsService.GetForCar(car.Id)
                    .Select(x => new TariffDTO(x.Id, x.Name))
                    .ToArrayAsync(),
                car.YearOfManufacture,
                car.Brand,
                car.Model,
                car.AdditionalInfo
            ));
        }

        protected override async Task VerifyOverride(GetCarInfoQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
