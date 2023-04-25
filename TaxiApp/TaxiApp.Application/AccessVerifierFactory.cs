using Microsoft.Extensions.DependencyInjection;

namespace TaxiApp.Application
{
    internal sealed class AccessVerifierFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public AccessVerifierFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public AccessVerifier Create()
        {
            return _serviceProvider.GetRequiredService<AccessVerifier>();
        }
    }
}
