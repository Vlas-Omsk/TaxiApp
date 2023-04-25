using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Abstractions;

namespace TaxiApp.DAL
{
    public sealed class MigrationsRunner : IMigrationsRunner
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MigrationsRunner(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task Run()
        {
            return _applicationDbContext.Database.MigrateAsync();
        }
    }
}
