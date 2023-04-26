using TaxiApp.DataTypes;

namespace TaxiApp.Domain.Abstractions.Models
{
    public sealed record UserInfo
    {
        public UserInfo(string login, UserRole role)
        {
            Login = login;
            Role = role;
        }

        public string Login { get; }
        public UserRole Role { get; }
    }
}
