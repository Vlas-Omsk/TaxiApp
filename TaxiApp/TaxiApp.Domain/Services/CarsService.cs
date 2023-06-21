using TaxiApp.DAL;
using TaxiApp.DAL.Entities;

namespace TaxiApp.Domain.Services
{
    public sealed class CarsService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CarsService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<CarEntity> GetAll()
        {
            return _applicationDbContext.Cars;
        }
    }
}
