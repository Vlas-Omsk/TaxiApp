using TaxiApp.Application.Abstractions;

namespace TaxiApp.Application.Version1_0.Commands
{
    public sealed record DeleteOrderCommand(
        int Id
    ) : IRequest<bool>;
}
