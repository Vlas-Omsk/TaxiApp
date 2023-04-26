using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Abstractions.Entities
{
    public sealed class DriverEntity
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public int TariffId { get; set; }
        public DriverState State { get; set; }
        public int CarId { get; set; }
        public TariffEntity Tariff { get; set; }
        public CarEntity Car { get; set; }
        public ICollection<OrderEntity> Orders { get; } = new List<OrderEntity>();
    }
}
