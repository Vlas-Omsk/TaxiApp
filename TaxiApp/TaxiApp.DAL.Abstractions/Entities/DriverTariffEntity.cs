namespace TaxiApp.DAL.Entities
{
    public sealed class DriverTariffEntity
    {
        public int DriverId { get; set; }
        public DriverEntity Driver { get; set; }
        public int TariffId { get; set; }
        public TariffEntity Tariff { get; set; }
    }
}
