using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record UserInfoDTO(
        string Login,
        FullName FullName,
        string Inn,
        string Passport,
        string Address,
        UserRole Role,
        string AdditionalInfo
    );
}
