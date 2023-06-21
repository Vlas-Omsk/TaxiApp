using TaxiApp.Application.Abstractions;
using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record UpdateDriverCommand(
        int Id,
        FullName FullName,
        string Inn,
        string Passport,
        string Address,
        string AdditionalInfo,
        int? CarId
    ) : IRequest<bool>;
}
