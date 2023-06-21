namespace TaxiApp.WindowsApp.Models
{
    public sealed class DriverModel
    {
        public DriverModel(
            int id,
            string fullName,
            string status,
            string tariff,
            string additionalInfo
        )
        {
            Id = id;
            FullName = fullName;
            Status = status;
            Tariff = tariff;
            AdditionalInfo = additionalInfo;
        }

        public int Id { get; }
        public string FullName { get; }
        public string Status { get; }
        public string Tariff { get; }
        public string AdditionalInfo { get; }
    }
}
