using Microsoft.Extensions.DependencyInjection;
using TaxiApp.Application.Abstractions;

namespace TaxiApp.Client.Direct
{
    public sealed class DirectClient : IClient
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestContext _requestContext;

        public DirectClient(
            IServiceProvider serviceProvider,
            RequestContext requestContext
        )
        {
            _serviceProvider = serviceProvider;
            _requestContext = requestContext;
        }

        public bool IsConnected { get; private set; }

        public Task SetAuthorization(string login, string password)
        {
            _requestContext.Login = login;
            _requestContext.Password = password;

            return Task.CompletedTask;
        }

        public async Task Connect()
        {
            using var scope = _serviceProvider.CreateScope();
            var applicationDbContextAccessor = scope.ServiceProvider.GetRequiredService<IApplicationDbContextAccessor>();

            await applicationDbContextAccessor.Migrate();

            IsConnected = true;
        }

        public async Task<Response<TResult>> Send<TResult>(IRequest<TResult> request)
        {
            using var scope = _serviceProvider.CreateScope();
            var requestExecutor = scope.ServiceProvider.GetRequiredService<IRequestExecutor>();

            if (!IsConnected)
                throw new InvalidOperationException();

            return await requestExecutor.Execute(request);
        }
    }
}
