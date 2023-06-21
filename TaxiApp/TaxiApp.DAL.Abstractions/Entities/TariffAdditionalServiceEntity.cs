namespace TaxiApp.DAL.Entities
{
    public sealed class TariffAdditionalServiceEntity
    {
        public int TariffId { get; set; }
        public TariffEntity Tariff { get; set; }
        public int AdditionalServiceId { get; set; }
        public AdditionalServiceEntity AdditionalService { get; set; }
    }
}
