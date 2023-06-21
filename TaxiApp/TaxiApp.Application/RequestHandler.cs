using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application
{
    public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest>
        where TRequest : IRequest<TResult>
    {
        private readonly AccessVerifierFactory _accessVerifierFactory;

        protected RequestHandler(AccessVerifierFactory accessVerifierFactory)
        {
            _accessVerifierFactory = accessVerifierFactory;
        }

        public async Task<IResponse> Execute(IRequest request)
        {
            var requestObj = (TRequest)request;

            return await ExecuteOverride(requestObj);
        }

        protected abstract Task<Response<TResult>> ExecuteOverride(TRequest request);

        public async Task<IResponse> Verify(IRequest request)
        {
            var requestObj = (TRequest)request;
            var accessVerifier = _accessVerifierFactory.Create();

            await VerifyOverride(requestObj, accessVerifier);

            if (accessVerifier.HasErrors)
                return Results.Fail<TResult>(accessVerifier.Error);

            return null;
        }

        protected abstract Task VerifyOverride(TRequest request, AccessVerifier verifier);

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
