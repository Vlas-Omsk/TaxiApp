using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Drivers
{
    internal sealed class GetOrderDriversQueryHandler : RequestHandler<GetOrderDriversQuery, OrderDriverDTO[]>
    {
        private readonly DriversService _driversService;

        public GetOrderDriversQueryHandler(
            DriversService driversService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _driversService = driversService;
        }

        protected override async Task<Response<OrderDriverDTO[]>> ExecuteOverride(GetOrderDriversQuery request)
        {
            var result = await _driversService.GetAll()
                .Select(x => new OrderDriverDTO(
                    x.Id,
                    new FullName(
                        x.LastName,
                        x.FirstName,
                        x.Patronymic
                    ),
                    x.DriverTariffs
                        .Select(x => new OrderTariffDTO(
                            x.TariffId,
                            x.Tariff.Name
                        ))
                        .ToArray(),
                    new OrderCarDTO(
                        x.CarId,
                        x.Car.Number
                    )
                ))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetOrderDriversQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
