using TaxiApp.DataTypes;

namespace TaxiApp.WindowsApp.Models
{
    public sealed class UserModel
    {
        public UserModel(
            string login,
            FullName fullName,
            string role
        )
        {
            Login = login;
            FullName = fullName;
            Role = role;
        }

        public string Login { get; }
        public FullName FullName { get; }
        public string Role { get; }
    }
}
