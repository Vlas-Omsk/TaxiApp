using TaxiApp.DAL.Abstractions.Models;

namespace TaxiApp.DAL.Abstractions.Repositories
{
    public interface ICarsRepository
    {
        IAsyncEnumerable<CarInfoWithDriverFullNameModel> GetAllWithDriverFullName();
    }
}
