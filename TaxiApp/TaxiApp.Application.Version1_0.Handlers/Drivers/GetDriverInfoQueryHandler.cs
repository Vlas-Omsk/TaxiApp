using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Drivers
{
    internal sealed class GetDriverInfoQueryHandler : RequestHandler<GetDriverInfoQuery, DriverInfoDTO>
    {
        private readonly DriversService _driversService;

        public GetDriverInfoQueryHandler(
            DriversService driversService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _driversService = driversService;
        }

        protected override async Task<Response<DriverInfoDTO>> ExecuteOverride(GetDriverInfoQuery request)
        {
            var driver = await _driversService.GetAll()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (driver == null)
                return Fail(Errors.Drivers.NotFound);

            return Success(new DriverInfoDTO(
                new FullName(
                    driver.LastName,
                    driver.FirstName,
                    driver.Patronymic
                ),
                driver.Inn,
                driver.Passport,
                driver.Address,
                driver.AdditionalInfo
            ));
        }

        protected override async Task VerifyOverride(GetDriverInfoQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
