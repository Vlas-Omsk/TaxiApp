namespace TaxiApp.DAL.Entities
{
    public sealed class OrderEntity
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public DriverEntity Driver { get; set; }
        public int CarId { get; set; }
        //public CarEntity Car { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public decimal Cost { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompleatedAt { get; set; }
        public int ClientId { get; set; }
        public ClientEntity Client { get; set; }
        public string Comment { get; set; }
        public int TariffId { get; set; }
        public TariffEntity Tariff { get; set; }
    }
}
