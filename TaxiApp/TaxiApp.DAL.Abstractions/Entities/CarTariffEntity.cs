namespace TaxiApp.DAL.Entities
{
    public sealed class CarTariffEntity
    {
        public int CarId { get; set; }
        public CarEntity Car { get; set; }
        public int TariffId { get; set; }
        public TariffEntity Tariff { get; set; }
    }
}
