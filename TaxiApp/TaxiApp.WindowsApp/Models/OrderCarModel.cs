namespace TaxiApp.WindowsApp.Models
{
    public sealed class OrderCarModel
    {
        public OrderCarModel(int id, string number)
        {
            Id = id;
            Number = number;
        }

        public int Id { get; }
        public string Number { get; }
    }
}
