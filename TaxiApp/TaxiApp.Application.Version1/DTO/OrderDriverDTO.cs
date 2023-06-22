using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record OrderDriverDTO(
        int Id,
        FullName FullName,
        OrderTariffDTO[] Tariffs,
        OrderCarDTO Car
    );
}
