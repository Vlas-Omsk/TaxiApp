using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Entities
{
    public sealed class UserEntity
    {
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
