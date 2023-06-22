namespace TaxiApp.WindowsApp.Models
{
    public sealed class OrderClientModel
    {
        public OrderClientModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
