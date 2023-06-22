using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Reports
{
    public sealed class GetReportForDriverOrdersQueryHandler : RequestHandler<GetReportForDriverOrdersQuery, ReportForDriverOrdersValueDTO[]>
    {
        private readonly ReportsService _reportsService;

        public GetReportForDriverOrdersQueryHandler(
            ReportsService reportsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _reportsService = reportsService;
        }

        protected override async Task<Response<ReportForDriverOrdersValueDTO[]>> ExecuteOverride(GetReportForDriverOrdersQuery request)
        {
            var result = await _reportsService.GetReportForDriverOrders(request.DriverId, request.FromDate, request.ToDate)
                .Select(x => new ReportForDriverOrdersValueDTO(
                    x.Id,
                    x.Cost
                ))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetReportForDriverOrdersQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
