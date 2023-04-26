using TaxiApp.DAL.Abstractions.Models;
using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Abstractions.Repositories
{
    public interface IDriversRepository
    {
        IAsyncEnumerable<DriverInfoModel> GetAll();
        IAsyncEnumerable<DriverInfoModel> GetAllByState(DriverState state);
    }
}
