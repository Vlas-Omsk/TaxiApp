using TaxiApp.DAL;
using TaxiApp.DAL.Entities;

namespace TaxiApp.Domain.Services
{
    public sealed class DriversService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DriversService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<DriverEntity> GetAll()
        {
            return _applicationDbContext.Drivers;
        }
    }
}
