using TaxiApp.DAL;
using TaxiApp.DAL.Entities;

namespace TaxiApp.Domain.Services
{
    public sealed class ClientsService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ClientsService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<ClientEntity> GetAll()
        {
            return _applicationDbContext.Clients;
        }
    }
}
