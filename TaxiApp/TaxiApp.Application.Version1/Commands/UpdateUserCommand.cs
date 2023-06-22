using TaxiApp.Application.Abstractions;
using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record UpdateUserCommand(
        string Login,
        string Password,
        FullName FullName,
        string Inn,
        string Passport,
        string Address,
        UserRole? Role,
        string AdditionalInfo
    ) : IRequest<bool>;
}
