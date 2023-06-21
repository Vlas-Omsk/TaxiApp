using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TaxiApp.DAL.SqlServer
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, string mainDatabaseConnectionString)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext >(
                x => x.UseSqlServer(mainDatabaseConnectionString)
            );
        }
    }
}
