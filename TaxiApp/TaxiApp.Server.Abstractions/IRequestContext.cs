namespace TaxiApp.Server.Abstractions
{
    public interface IRequestContext
    {
        string Login { get; }
        string Password { get; }
    }
}