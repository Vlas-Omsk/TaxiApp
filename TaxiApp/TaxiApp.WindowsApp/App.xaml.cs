using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using TaxiApp.Application.Extensions;
using TaxiApp.Client.Direct;
using TaxiApp.DAL;
using TaxiApp.DAL.Abstractions;
using TaxiApp.Domain.Extensions;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp
{
    public partial class App : System.Windows.Application
    {
        public new static App Current => (App)System.Windows.Application.Current;
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ConfigureServices();

            var migrationsRunner = ServiceProvider.GetRequiredService<IMigrationsRunner>();

            migrationsRunner.Run().GetAwaiter().GetResult();

            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();

            navigationService.NavigateTo(new StartupView());
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<NavigationService>();
            services.AddSingleton<SessionService>();
            services.AddSingleton<ResourcesService>();

            var viewModelsTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x =>
                    x.GetCustomAttribute<ViewModelAttribute>() != null
                );

            foreach (var type in viewModelsTypes)
                services.AddTransient(type);

            services.AddLogging();
            services.AddDataAccess("Server=192.168.1.100;Database=TaxiAppDB1;TrustServerCertificate=True;User Id=sa;Password=roooot");
            services.AddDomain();
            services.AddApplication();
            services.AddDirectClient();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
