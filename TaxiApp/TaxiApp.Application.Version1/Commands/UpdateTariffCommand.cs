using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record UpdateTariffCommand(
        int Id,
        string Name,
        decimal? StartingPrice,
        TimeSpan? FreeWaiting,
        decimal? PaidWaitingPricePerMin,
        decimal? InCityPricePerKm,
        decimal? OutsideCityPricePerKm,
        decimal? WaitingOnWayPricePerMin,
        string Description
    ) : IRequest<bool>;
}
