using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application
{
    internal interface IRequestAccess<out TRequest> where TRequest : IRequest
    {
        Task<IResponse> Verify(IRequest request);
    }
}
