using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Tariffs
{
    internal sealed class GetTariffInfoQueryHandler : RequestHandler<GetTariffInfoQuery, TariffInfoDTO>
    {
        private readonly TariffsService _tariffsService;

        public GetTariffInfoQueryHandler(
            TariffsService tariffsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _tariffsService = tariffsService;
        }

        protected override async Task<Response<TariffInfoDTO>> ExecuteOverride(GetTariffInfoQuery request)
        {
            var tariff = await _tariffsService.GetAll()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Success(new TariffInfoDTO(
                tariff.Id,
                tariff.Name,
                tariff.StartingPrice,
                tariff.FreeWaiting,
                tariff.PaidWaitingPricePerMin,
                tariff.InCityPricePerKm,
                tariff.OutsideCityPricePerKm,
                tariff.WaitingOnWayPricePerMin,
                tariff.Description
            ));
        }

        protected override async Task VerifyOverride(GetTariffInfoQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
        }
    }
}
