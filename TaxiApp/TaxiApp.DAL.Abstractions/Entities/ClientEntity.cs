namespace TaxiApp.DAL.Abstractions.Entities
{
    public sealed class ClientEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    }
}
