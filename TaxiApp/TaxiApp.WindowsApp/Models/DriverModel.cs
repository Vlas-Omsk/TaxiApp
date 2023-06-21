namespace TaxiApp.WindowsApp.Models
{
    public sealed class DriverModel
    {
        public DriverModel(
            string fullName,
            string status,
            string tariff,
            string additionalInfo
        )
        {
            FullName = fullName;
            Status = status;
            Tariff = tariff;
            AdditionalInfo = additionalInfo;
        }

        public string FullName { get; }
        public string Status { get; }
        public string Tariff { get; }
        public string AdditionalInfo { get; }
    }
}
