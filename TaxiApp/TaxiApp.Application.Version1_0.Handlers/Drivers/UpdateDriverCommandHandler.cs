using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Drivers
{
    internal sealed class UpdateDriverCommandHandler : RequestHandler<UpdateDriverCommand, bool>
    {
        private readonly DriversService _driversService;

        public UpdateDriverCommandHandler(
            DriversService driversService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _driversService = driversService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(UpdateDriverCommand request)
        {
            await _driversService.Update(
                request.Id,
                request.FullName,
                request.Inn,
                request.Passport,
                request.Address,
                request.AdditionalInfo,
                request.CarId
            );

            return Success(true);
        }

        protected override async Task VerifyOverride(UpdateDriverCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
