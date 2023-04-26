using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1.DTO;

namespace TaxiApp.Application.Version1.Queries
{
    public sealed record GetCarsQuery : IRequest<CarDTO[]>
    {
    }
}
