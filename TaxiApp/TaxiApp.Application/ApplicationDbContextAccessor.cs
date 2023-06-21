using TaxiApp.Application.Abstractions;
using TaxiApp.DAL;

namespace TaxiApp.Application
{
    public sealed class ApplicationDbContextAccessor : IApplicationDbContextAccessor
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ApplicationDbContextAccessor(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task Migrate()
        {
            return _applicationDbContext.Migrate();
        }
    }
}
