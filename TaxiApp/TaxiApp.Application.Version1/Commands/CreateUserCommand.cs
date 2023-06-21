using TaxiApp.Application.Abstractions;
using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record CreateUserCommand(
        string Login,
        string Password,
        UserRole Role
    ) : IRequest<bool>;
}
