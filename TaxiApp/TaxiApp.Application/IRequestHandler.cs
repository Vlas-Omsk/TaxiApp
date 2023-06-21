using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application
{
    internal interface IRequestHandler<out TRequest> where TRequest : IRequest
    {
        Task<IResponse> Execute(IRequest request);
        Task<IResponse> Verify(IRequest request);
    }
}
