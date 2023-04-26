using TaxiApp.Domain.Abstractions.Models;

namespace TaxiApp.Domain.Abstractions.Services
{
    public interface ICarsService
    {
        IAsyncEnumerable<CarInfo> GetAllWithDriverFullName();
    }
}
