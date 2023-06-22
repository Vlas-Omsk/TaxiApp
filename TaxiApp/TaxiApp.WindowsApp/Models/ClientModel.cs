namespace TaxiApp.WindowsApp.Models
{
    public sealed class ClientModel
    {
        public ClientModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
