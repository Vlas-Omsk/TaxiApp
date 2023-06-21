using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Tariffs
{
    internal sealed class DeleteTariffCommandHandler : RequestHandler<DeleteTariffCommand, bool>
    {
        private readonly TariffsService _tariffsService;

        public DeleteTariffCommandHandler(
            TariffsService tariffsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _tariffsService = tariffsService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(DeleteTariffCommand request)
        {
            await _tariffsService.Delete(request.Id);

            return Success(true);
        }

        protected override async Task VerifyOverride(DeleteTariffCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
