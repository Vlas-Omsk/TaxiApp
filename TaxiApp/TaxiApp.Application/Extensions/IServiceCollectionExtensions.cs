using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;
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

            services.AddTypesWithInterface("TaxiApp.Application.Version1", "IRequestAccess`1");
            services.AddTypesWithInterface("TaxiApp.Application.Version1", "IRequestHandler`1");
        }

        private static void AddTypesWithInterface(this IServiceCollection services, string @namespace, string name)
        {
            var requestTypes2 = Assembly.GetExecutingAssembly()
                .GetTypes();

            var requestTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetCustomAttribute<CompilerGeneratedAttribute>() == null)
                .Where(x => x.Namespace != null && x.Namespace.StartsWith(@namespace))
                .Where(x => x.GetInterface(name) != null);

            foreach (var requestType in requestTypes)
                services.AddScoped(requestType.GetInterface(name), requestType);
        }
    }
}
