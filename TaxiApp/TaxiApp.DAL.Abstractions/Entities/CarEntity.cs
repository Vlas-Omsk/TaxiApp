namespace TaxiApp.DAL.Abstractions.Entities
{
    public sealed class CarEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
        public ICollection<DriverEntity> Drivers { get; } = new List<DriverEntity>();
    }
}
