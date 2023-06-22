using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL;
using TaxiApp.Domain.Models;

namespace TaxiApp.Domain.Services
{
    public sealed class ReportsService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ReportsService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IAsyncEnumerable<ReportForDriverOrdersValue> GetReportForDriverOrders(
            int driverId,
            DateTime fromDate,
            DateTime toDate
        )
        {
            return _applicationDbContext.Orders
                .Where(x => x.DriverId == driverId)
                .Where(x => x.CreatedAt >= fromDate && x.CreatedAt <= toDate)
                .Select(x => new ReportForDriverOrdersValue(
                    x.Id,
                    x.Cost
                ))
                .AsAsyncEnumerable();
        }
    }
}
