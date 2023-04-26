namespace TaxiApp.DAL.Abstractions.Models
{
    public sealed record CarInfoWithDriverFullNameModel
    {
        public CarInfoWithDriverFullNameModel(
            int id,
            string brand,
            string number,
            string color,
            string driverFullName
        )
        {
            Id = id;
            Brand = brand;
            Number = number;
            Color = color;
            DriverFullName = driverFullName;
        }

        public int Id { get; }
        public string Brand { get; }
        public string Number { get; }
        public string Color { get; }
        public string DriverFullName { get; }
    }
}
