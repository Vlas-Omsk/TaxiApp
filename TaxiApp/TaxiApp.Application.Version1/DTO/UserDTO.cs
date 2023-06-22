using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record UserDTO(
        string Login,
        FullName FullName,
        UserRole Role
    );
}
