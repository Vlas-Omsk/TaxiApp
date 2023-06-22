using Microsoft.Extensions.DependencyInjection;
using TaxiApp.Domain.Services;

namespace TaxiApp.Domain.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<UsersService>();
            services.AddScoped<CarsService>();
            services.AddScoped<DriversService>();
            services.AddScoped<TariffsService>();
            services.AddScoped<OrdersService>();
            services.AddScoped<ClientsService>();
            services.AddScoped<ReportsService>();
        }
    }
}
