namespace TaxiApp.WindowsApp.Services
{
    internal sealed class ResourcesService
    {
        public string FindString(string key)
        {
            var resource = App.Current.Resources[key];

            if (resource == null)
                return $"Undefined resource '{key}'";

            return (string)resource;
        }
    }
}
