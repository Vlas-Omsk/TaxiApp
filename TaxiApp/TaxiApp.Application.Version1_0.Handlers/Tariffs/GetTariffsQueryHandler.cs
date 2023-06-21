using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Tariffs
{
    internal sealed class GetTariffsQueryHandler : RequestHandler<GetTariffsQuery, TariffDTO[]>
    {
        private readonly TariffsService _tariffsService;

        public GetTariffsQueryHandler(
            TariffsService tariffsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _tariffsService = tariffsService;
        }

        protected override async Task<Response<TariffDTO[]>> ExecuteOverride(GetTariffsQuery request)
        {
            var result = await _tariffsService.GetAll()
                .Select(x => new TariffDTO(x.Id, x.Name))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetTariffsQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
        }
    }
}
