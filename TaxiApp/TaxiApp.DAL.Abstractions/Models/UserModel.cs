using TaxiApp.DataTypes;

namespace TaxiApp.DAL.Abstractions.Models
{
    public sealed class UserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
