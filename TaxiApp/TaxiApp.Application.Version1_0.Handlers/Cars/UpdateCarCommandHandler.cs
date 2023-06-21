using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Cars
{
    internal sealed class UpdateCarCommandHandler : RequestHandler<UpdateCarCommand, bool>
    {
        private readonly CarsService _carsService;

        public UpdateCarCommandHandler(
            CarsService carsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _carsService = carsService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(UpdateCarCommand request)
        {
            await _carsService.Update(
                request.Id,
                request.Number,
                request.Color,
                request.TariffIds,
                request.YearOfManufacture,
                request.Brand,
                request.Model,
                request.AdditionalInfo
            );

            return Success(true);
        }

        protected override async Task VerifyOverride(UpdateCarCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
