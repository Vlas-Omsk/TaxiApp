using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Entities
{
    public sealed class DriverEntity
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Inn { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public DriverState State { get; set; }
        public string AdditionalInfo { get; set; }
        public int CarId { get; set; }
        public CarEntity Car { get; set; }
        public ICollection<OrderEntity> Orders { get; } = new List<OrderEntity>();
        public ICollection<DriverTariffEntity> DriverTariffs { get; } = new List<DriverTariffEntity>();
    }
}
