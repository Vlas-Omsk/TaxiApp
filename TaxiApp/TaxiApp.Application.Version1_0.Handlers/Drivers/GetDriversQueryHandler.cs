using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Services;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Drivers
{
    internal sealed class GetDriversQueryHandler : RequestHandler<GetDriversQuery, DriverDTO[]>
    {
        private readonly SecurityService _securityService;
        private readonly DriversService _driversService;

        public GetDriversQueryHandler(
            DriversService driversService,
            AccessVerifierFactory accessVerifierFactory,
            SecurityService securityService
        ) : base(accessVerifierFactory)
        {
            _driversService = driversService;
            _securityService = securityService;
        }

        protected override async Task<Response<DriverDTO[]>> ExecuteOverride(GetDriversQuery request)
        {
            var queryable = _driversService.GetAll();

            if (request.FilterActive)
                queryable = queryable.Where(x => x.State == DriverState.Active);

            var result = await queryable
                .Select(x => new DriverDTO(
                    x.Id,
                    new FullName(
                        x.LastName,
                        x.FirstName,
                        x.Patronymic
                    ),
                    x.State,
                    x.Tariff.Name
                ))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetDriversQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);

            var employee = await _securityService.GetCurrentUser();

            if (employee.Role == UserRole.Dispatcher && request.FilterActive == false)
                verifier.Error = Errors.Users.DoesNotHaveAccess;
        }
    }
}
