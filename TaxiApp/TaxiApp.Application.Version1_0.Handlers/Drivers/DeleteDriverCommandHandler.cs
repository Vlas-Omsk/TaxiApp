using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Drivers
{
    internal sealed class DeleteDriverCommandHandler : RequestHandler<DeleteDriverCommand, bool>
    {
        private readonly DriversService _driversService;

        public DeleteDriverCommandHandler(
            DriversService driversService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _driversService = driversService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(DeleteDriverCommand request)
        {
            await _driversService.Delete(request.Id);

            return Success(true);
        }

        protected override async Task VerifyOverride(DeleteDriverCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
