using Microsoft.Extensions.DependencyInjection;
using TaxiApp.Client.Abstractions;
using TaxiApp.Server.Abstractions;

namespace TaxiApp.Client.Direct
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDirectClient(this IServiceCollection services)
        {
            var requestContext = new RequestContext();

            services.AddSingleton<IRequestContext>(requestContext);
            services.AddSingleton<IClient>(x => new DirectClient(
                x.GetRequiredService<IServiceProvider>(),
                requestContext
            ));
        }
    }
}
