namespace TaxiApp.WindowsApp.Models
{
    public sealed class OrderModel
    {
        public OrderModel(
            int id,
            string createdAtDateText,
            string createdAtTimeText,
            string driverFullName
        )
        {
            Id = id;
            CreatedAtDateText = createdAtDateText;
            CreatedAtTimeText = createdAtTimeText;
            DriverFullName = driverFullName;
        }

        public int Id { get; set; }
        public string CreatedAtDateText { get; set; }
        public string CreatedAtTimeText { get; set; }
        public string DriverFullName { get; set; }
    }
}
