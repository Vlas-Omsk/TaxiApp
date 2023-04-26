using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Abstractions.Models
{
    public sealed record UserInfoModel
    {
        public UserInfoModel(string login, UserRole role)
        {
            Login = login;
            Role = role;
        }

        public string Login { get; }
        public UserRole Role { get; }
    }
}
