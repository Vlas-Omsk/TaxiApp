using Microsoft.Extensions.DependencyInjection;
using TaxiApp.Domain.Abstractions.Services;
using TaxiApp.Domain.Services;

namespace TaxiApp.Domain.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
