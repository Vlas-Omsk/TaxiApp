using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application
{
    internal sealed class RequestExecutor : IRequestExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public RequestExecutor(
            IServiceProvider serviceProvider,
            ILogger<RequestExecutor> logger
        )
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<Response<TResult>> Execute<TResult>(IRequest<TResult> request)
        {
            _logger.LogInformation("Executing request: {name}", request.GetType().Name);

            var response = await ExecuteInternal<IRequest<TResult>, Response<TResult>>(request);

            _logger.LogInformation(
                "Executed response: {name}, Success: {success}, ErrorType: {errorType}, ErrorMessage: {errorMessage}",
                request.GetType().Name,
                response.Success,
                response.Error?.Type,
                response.Error?.Message
            );

            return response;
        }

        private async Task<TResponse> ExecuteInternal<TRequest, TResponse>(TRequest request) where TRequest : IRequest where TResponse : IResponse
        {
            var requestType = request.GetType();

            var handler = (IRequestHandler<TRequest>)_serviceProvider
                .GetRequiredService(typeof(IRequestHandler<>)
                .MakeGenericType(requestType));

            var response = await handler.Verify(request);

            if (response != null)
                return (TResponse)response;

            return (TResponse)await handler.Execute(request);
        }
    }
}
