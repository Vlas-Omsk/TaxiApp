namespace TaxiApp.Application.Abstractions
{
    public interface IRequest
    {
    }

    public interface IRequest<TResult> : IRequest
    {
    }
}
