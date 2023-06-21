using TaxiApp.DataTypes;

namespace TaxiApp.Domain.Models
{
    public sealed record User(
        string Login,
        UserRole Role
    );
}
