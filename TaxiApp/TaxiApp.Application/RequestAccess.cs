using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application
{
    internal abstract class RequestAccess<TRequest, TResult> : IRequestAccess<TRequest> where TRequest : IRequest<TResult>
    {
        private readonly AccessVerifierFactory _accessVerifierFactory;

        protected RequestAccess(AccessVerifierFactory accessVerifierFactory)
        {
            _accessVerifierFactory = accessVerifierFactory;
        }

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
    }
}
