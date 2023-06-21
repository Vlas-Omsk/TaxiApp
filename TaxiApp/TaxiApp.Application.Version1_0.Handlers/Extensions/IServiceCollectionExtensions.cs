using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TaxiApp.Application.Version1_0.Handlers
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationVersion1_0(this IServiceCollection services)
        {
            services.AddTypesWithInterface("TaxiApp.Application.Version1_0.Handlers", "IRequestHandler`1");
        }

        private static void AddTypesWithInterface(this IServiceCollection services, string @namespace, string name)
        {
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
