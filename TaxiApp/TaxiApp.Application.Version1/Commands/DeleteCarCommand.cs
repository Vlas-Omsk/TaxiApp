using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record DeleteCarCommand(
        int Id
    ) : IRequest<bool>;
}
