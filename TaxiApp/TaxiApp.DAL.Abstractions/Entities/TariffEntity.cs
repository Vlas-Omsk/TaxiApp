namespace TaxiApp.DAL.Abstractions.Entities
{
    public sealed class TariffEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingPrice { get; set; }
        public string Description { get; set; }
        public ICollection<DriverEntity> Drivers { get; } = new List<DriverEntity>();
    }
}
