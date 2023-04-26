namespace TaxiApp.DAL.Entities
{
    internal sealed class CarEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
        public ICollection<DriverEntity> Drivers { get; } = new List<DriverEntity>();
    }
}
