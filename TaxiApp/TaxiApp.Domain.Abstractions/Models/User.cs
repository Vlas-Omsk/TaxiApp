using TaxiApp.DataTypes;

namespace TaxiApp.Domain.Abstractions.Models
{
    public sealed record User
    {
        public User(string login, string password, UserRole role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public string Login { get; }
        public string Password { get; }
        public UserRole Role { get; }
    }
}
