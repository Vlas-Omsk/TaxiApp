namespace TaxiApp.Domain.Abstractions.Models
{
    public sealed class CarInfo
    {
        public CarInfo(int id,
            string brand,
            string number,
            string color,
            FullName[] driverFullNames
        )
        {
            Id = id;
            Brand = brand;
            Number = number;
            Color = color;
            DriverFullNames = driverFullNames;
        }

        public int Id { get; }
        public string Brand { get; }
        public string Number { get; }
        public string Color { get; }
        public FullName[] DriverFullNames { get; }
    }
}
