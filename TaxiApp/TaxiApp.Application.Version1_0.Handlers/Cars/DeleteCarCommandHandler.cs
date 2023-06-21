using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Cars
{
    internal sealed class DeleteCarCommandHandler : RequestHandler<DeleteCarCommand, bool>
    {
        private readonly CarsService _carsService;

        public DeleteCarCommandHandler(
            CarsService carsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _carsService = carsService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(DeleteCarCommand request)
        {
            await _carsService.Delete(request.Id);

            return Success(true);
        }

        protected override async Task VerifyOverride(DeleteCarCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
