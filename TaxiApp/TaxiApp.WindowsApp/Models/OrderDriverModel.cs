using TaxiApp.DataTypes;

namespace TaxiApp.WindowsApp.Models
{
    public sealed class OrderDriverModel
    {
        public OrderDriverModel(
            int id,
            FullName fullName,
            OrderTariffModel[] tariffs,
            OrderCarModel car
        )
        {
            Id = id;
            FullName = fullName;
            Tariffs = tariffs;
            Car = car;
        }

        public int Id { get; }
        public FullName FullName { get; }
        public OrderTariffModel[] Tariffs { get; }
        public OrderCarModel Car { get; }
    }
}
