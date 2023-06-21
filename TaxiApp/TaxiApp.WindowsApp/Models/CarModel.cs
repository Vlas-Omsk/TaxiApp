namespace TaxiApp.WindowsApp.Models
{
    public sealed class CarModel
    {
        public CarModel(
            int id,
            string govermentNumber,
            string driverFullName,
            string model,
            string color,
            string additionalInfo
        )
        {
            Id = id;
            GovermentNumber = govermentNumber;
            DriverFullName = driverFullName;
            Model = model;
            Color = color;
            AdditionalInfo = additionalInfo;
        }

        public int Id { get; }
        public string GovermentNumber { get; }
        public string DriverFullName { get; }
        public string Model { get; }
        public string Color { get; }
        public string AdditionalInfo { get; }
    }
}
