namespace TaxiApp.DAL.Entities
{
    public sealed class TariffEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingPrice { get; set; }
        public string Description { get; set; }
        public ICollection<CarTariffEntity> CarTariffs { get; } = new List<CarTariffEntity>();
        public ICollection<DriverTariffEntity> DriverTariffs { get; } = new List<DriverTariffEntity>();
    }
}
