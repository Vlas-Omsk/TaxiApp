using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Cars
{
    internal sealed class GetCarsQueryHandler : RequestHandler<GetCarsQuery, CarDTO[]>
    {
        private readonly CarsService _carsService;

        public GetCarsQueryHandler(
            CarsService carsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _carsService = carsService;
        }

        protected override async Task<Response<CarDTO[]>> ExecuteOverride(GetCarsQuery request)
        {
            var result = await _carsService.GetAll()
                .Select(x => new CarDTO(
                    x.Id,
                    x.Brand,
                    x.Number,
                    x.Color,
                    string.Join(
                        ", ",
                        x.Drivers.Select(x => $"{x.LastName} {x.FirstName} {x.Patronymic}")
                    )
                ))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetCarsQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
