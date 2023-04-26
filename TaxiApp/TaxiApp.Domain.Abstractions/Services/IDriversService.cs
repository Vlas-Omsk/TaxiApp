using TaxiApp.Domain.Abstractions.Models;

namespace TaxiApp.Domain.Abstractions.Services
{
    public interface IDriversService
    {
        IAsyncEnumerable<DriverInfo> GetAll(bool filterActive);
    }
}
