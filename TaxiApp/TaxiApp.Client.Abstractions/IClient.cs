using TaxiApp.Application.Abstractions;

namespace TaxiApp.Client.Abstractions
{
    public interface IClient
    {
        bool IsConnected { get; }

        Task SetAuthorization(string login, string password);
        Task Connect();
        Task<Response<TResult>> Send<TResult>(IRequest<TResult> request);
    }
}