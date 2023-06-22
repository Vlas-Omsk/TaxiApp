using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Entities
{
    public sealed class UserEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Inn { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
