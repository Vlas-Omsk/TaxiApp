using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record UpdateOrderCommand(
        int Id,
        int? DriverId,
        int? ClientId,
        decimal? Cost,
        string AddressFrom,
        string AddressTo,
        string Comment
    ) : IRequest<bool>;
}
