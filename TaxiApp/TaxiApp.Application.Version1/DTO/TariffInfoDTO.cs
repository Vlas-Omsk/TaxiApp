namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record TariffInfoDTO(
        int Id,
        string Name,
        decimal StartingPrice,
        TimeSpan FreeWaiting,
        decimal PaidWaitingPricePerMin,
        decimal InCityPricePerKm,
        decimal OutsideCityPricePerKm,
        decimal WaitingOnWayPricePerMin,
        string Description
    );
}
