namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record OrderInfoDTO(
        int Id,
        int DriverId,
        int ClientId,
        decimal Cost,
        string AddressFrom,
        string AddressTo,
        DateTime CreatedAt,
        string Comment
    );
}
