namespace TaxiApp.WindowsApp.Services
{
    internal sealed class SessionService
    {
        public User User { get; set; }

        public void SignIn(User user)
        {
            User = user;
        }
    }
}
