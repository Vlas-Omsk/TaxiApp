using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Tariffs
{
    internal sealed class UpdateTariffCommandHandler : RequestHandler<UpdateTariffCommand, bool>
    {
        private readonly TariffsService _tariffsService;

        public UpdateTariffCommandHandler(
            TariffsService tariffsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _tariffsService = tariffsService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(UpdateTariffCommand request)
        {
            await _tariffsService.Update(
                request.Id,
                request.Name,
                request.StartingPrice,
                request.FreeWaiting,
                request.PaidWaitingPricePerMin,
                request.InCityPricePerKm,
                request.OutsideCityPricePerKm,
                request.WaitingOnWayPricePerMin,
                request.Description
            );

            return Success(true);
        }

        protected override async Task VerifyOverride(UpdateTariffCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
