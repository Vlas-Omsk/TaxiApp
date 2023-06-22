using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record OrderDTO(
        int Id,
        DateTime CreatedAt,
        FullName DriverFullName
    );
}
