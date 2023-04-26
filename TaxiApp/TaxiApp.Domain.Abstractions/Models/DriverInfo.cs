using TaxiApp.DataTypes;

namespace TaxiApp.Domain.Abstractions.Models
{
    public sealed record DriverInfo
    {
        public DriverInfo(
            int id,
            FullName fullName,
            DriverState state,
            string tariffName
        )
        {
            Id = id;
            FullName = fullName;
            State = state;
            TariffName = tariffName;
        }

        public int Id { get; }
        public FullName FullName { get; }
        public DriverState State { get; }
        public string TariffName { get; }
    }
}
