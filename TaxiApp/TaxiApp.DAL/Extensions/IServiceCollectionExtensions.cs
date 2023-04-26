using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiApp.DAL.Abstractions;
using TaxiApp.DAL.Abstractions.Repositories;
using TaxiApp.DAL.Repositories;

namespace TaxiApp.DAL
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, string mainDatabaseConnectionString)
        {
            services.AddDbContext<ApplicationDbContext>(x =>
                x.UseSqlServer(mainDatabaseConnectionString)
            );
            services.AddScoped<IMigrationsRunner, MigrationsRunner>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICarsRepository, CarsRepository>();
            services.AddScoped<IDriversRepository, DriversRepository>();
        }
    }
}
