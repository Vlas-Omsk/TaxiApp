namespace TaxiApp.DAL.Entities
{
    public sealed class AdditionalServiceEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<TariffAdditionalServiceEntity> TariffAdditionalServices { get; } = new List<TariffAdditionalServiceEntity>();
    }
}
