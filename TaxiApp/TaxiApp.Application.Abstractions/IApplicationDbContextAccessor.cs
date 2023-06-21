namespace TaxiApp.Application.Abstractions
{
    public interface IApplicationDbContextAccessor
    {
        Task Migrate();
    }
}
