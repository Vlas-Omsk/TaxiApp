using System.Threading.Tasks;
using TaxiApp.Application.Abstractions;
using TaxiApp.Client.Abstractions;

namespace TaxiApp.WindowsApp.Services
{
    internal sealed class ApiService
    {
        private readonly IClient _client;

        public ApiService(IClient client)
        {
            _client = client;
        }

        public Task SetAuthorization(string login, string password)
        {
            return _client.SetAuthorization(login, password);
        }

        public Task Connect()
        {
            return _client.Connect();
        }

        public async Task<ApiResponse<TResult>> Send<TResult>(IRequest<TResult> request)
        {
            var response = await _client.Send(request);

            if (!response.Success)
            {
                var message = $"{response.Error.Type}: {response.Error.Message ?? "<empty>"}";

                return new ApiResponse<TResult>(
                    response.Value,
                    message,
                    false
                );
            }

            return new ApiResponse<TResult>(
                response.Value,
                null,
                true
            );
        }
    }
}
