namespace TaxiApp.WindowsApp.Models
{
    internal sealed class CarModel
    {
        public CarModel(
            string govermentNumber,
            string driverFullName,
            string model,
            string color,
            string additionalInfo
        )
        {
            GovermentNumber = govermentNumber;
            DriverFullName = driverFullName;
            Model = model;
            Color = color;
            AdditionalInfo = additionalInfo;
        }

        public string GovermentNumber { get; }
        public string DriverFullName { get; }
        public string Model { get; }
        public string Color { get; }
        public string AdditionalInfo { get; }
    }
}
