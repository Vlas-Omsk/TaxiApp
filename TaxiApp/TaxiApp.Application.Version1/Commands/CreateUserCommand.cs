using TaxiApp.Application.Abstractions;
using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1.Commands
{
    public sealed class CreateUserCommand : IRequest<bool>
    {
        public CreateUserCommand(string login, string password, UserRole role)
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
