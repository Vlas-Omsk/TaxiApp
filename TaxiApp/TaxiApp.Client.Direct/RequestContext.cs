using TaxiApp.Server.Abstractions;

namespace TaxiApp.Client.Direct
{
    public sealed class RequestContext : IRequestContext
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
