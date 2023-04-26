namespace TaxiApp.DAL.Entities
{
    internal sealed class TariffEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingPrice { get; set; }
        public string Description { get; set; }
        public ICollection<DriverEntity> Drivers { get; } = new List<DriverEntity>();
    }
}
