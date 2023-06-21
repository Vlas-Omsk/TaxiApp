using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record UpdateCarCommand(
        int Id,
        string Number,
        string Color,
        int[] TariffIds,
        int? YearOfManufacture,
        string Brand,
        string Model,
        string AdditionalInfo
    ) : IRequest<bool>;
}
