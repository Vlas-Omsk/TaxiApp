using Microsoft.Extensions.DependencyInjection;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Services;

namespace TaxiApp.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRequestExecutor, RequestExecutor>();
            services.AddTransient<AccessVerifier>();
            services.AddScoped<AccessVerifierFactory>();
            services.AddScoped<SecurityService>();
            services.AddScoped<IApplicationDbContextAccessor, ApplicationDbContextAccessor>();
        }
    }
}
