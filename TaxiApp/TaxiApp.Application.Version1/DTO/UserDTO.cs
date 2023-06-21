using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed class UserDTO
    {
        public UserDTO(string login, UserRole role)
        {
            Login = login;
            Role = role;
        }

        public string Login { get; }
        public UserRole Role { get; }
    }
}
