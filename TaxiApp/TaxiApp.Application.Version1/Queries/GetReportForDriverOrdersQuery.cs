using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;

namespace TaxiApp.Application.Version1_0.Queries
{
    public sealed record GetReportForDriverOrdersQuery(
        int DriverId,
        DateTime FromDate,
        DateTime ToDate
    ) : IRequest<ReportForDriverOrdersValueDTO[]>;
}
