namespace TaxiApp.Application.Abstractions
{
    public interface IRequestExecutor
    {
        Task<Response<TResult>> Execute<TResult>(IRequest<TResult> request);
    }
}
