using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Abstractions.Models
{
    public sealed record DriverInfoModel
    {
        public DriverInfoModel(
            int id,
            string lastName,
            string firstName,
            string patronymic,
            DriverState state,
            string tariffName
        )
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            State = state;
            TariffName = tariffName;
        }

        public int Id { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public string Patronymic { get; }
        public DriverState State { get; }
        public string TariffName { get; }
    }
}
