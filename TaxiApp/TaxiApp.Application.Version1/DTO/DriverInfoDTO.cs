using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record DriverInfoDTO(
        FullName FullName,
        string Inn,
        string Passport,
        string Address,
        string AdditionalInfo
    );
}
