using Microsoft.Extensions.DependencyInjection;
using TaxiApp.Domain.Services;

namespace TaxiApp.Domain.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<UsersService>();
            services.AddSingleton<CarsService>();
            services.AddSingleton<DriversService>();
        }
    }
}
