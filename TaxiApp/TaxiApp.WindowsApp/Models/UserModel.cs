using TaxiApp.DataTypes;

namespace TaxiApp.WindowsApp.Models
{
    public sealed class UserModel
    {
        public UserModel(
            string login,
            FullName fulllName,
            string role
        )
        {
            Login = login;
            FulllName = fulllName;
            Role = role;
        }

        public string Login { get; }
        public FullName FulllName { get; }
        public string Role { get; }
    }
}
