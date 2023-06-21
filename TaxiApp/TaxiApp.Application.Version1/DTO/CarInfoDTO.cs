namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record CarInfoDTO(
        int Id,
        string Number,
        string Color,
        TariffDTO[] Tariffs,
        int YearOfManufacture,
        string Brand,
        string Model,
        string AdditionalInfo
    );
}
