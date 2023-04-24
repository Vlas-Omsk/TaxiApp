using TaxiApp.DataTypes;

namespace TaxiApp.WindowsApp
{
    internal sealed class User
    {
        public User(UserRole role)
        {
            Role = role;
        }

        public UserRole Role { get; }
    }
}
