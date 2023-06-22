using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record CreateOrderCommand(
        int DriverId,
        int ClientId,
        int TariffId,
        decimal Cost,
        string AddressFrom,
        string AddressTo,
        string Comment
    ) : IRequest<CreateOrderResponseDTO>;
}
