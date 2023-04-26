using TaxiApp.Application.Services;
using TaxiApp.Application.Version1.DTO;
using TaxiApp.Application.Version1.Queries;
using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1.Drivers.Queries.GetDrivers
{
    internal sealed class GetDriversQueryAccess : RequestAccess<GetDriversQuery, DriverDTO[]>
    {
        private readonly SecurityService _securityService;

        public GetDriversQueryAccess(
            AccessVerifierFactory accessVerifierFactory,
            SecurityService securityService
        ) : base(accessVerifierFactory)
        {
            _securityService = securityService;
        }

        protected override async Task VerifyOverride(GetDriversQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);

            var employee = await _securityService.GetCurrentUserInfo();

            if (employee.Role == UserRole.Dispatcher && request.FilterActive == false)
                verifier.Error = Errors.Users.DoesNotHaveAccess;
        }
    }
}
