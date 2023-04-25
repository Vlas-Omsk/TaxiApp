using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application
{
    internal abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest> where TRequest : IRequest<TResult>
    {
        public async Task<IResponse> Execute(IRequest request)
        {
            var requestObj = (TRequest)request;

            return await ExecuteOverride(requestObj);
        }

        protected abstract Task<Response<TResult>> ExecuteOverride(TRequest request);

        public static Response<TResult> Fail(Error error)
        {
            return Results.Fail<TResult>(error);
        }

        public static Response<TResult> Success(TResult result)
        {
            return Results.Success(result);
        }
    }
}
