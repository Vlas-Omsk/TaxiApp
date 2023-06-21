using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record DriverDTO(
        int Id,
        FullName FullName,
        DriverState State,
        string TariffName
    );
}
