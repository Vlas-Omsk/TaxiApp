using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using TaxiApp.Application.Extensions;
using TaxiApp.Application.Version1_0.Handlers;
using TaxiApp.Client.Direct;
using TaxiApp.DAL.SqlServer;
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

            var themeService = ServiceProvider.GetRequiredService<ThemeService>();

            themeService.SetTheme(Theme.White);

            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();

            navigationService.NavigateTo(new StartupView());
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ApiService>();
            services.AddSingleton<NavigationService>();
            services.AddSingleton<SessionService>();
            services.AddSingleton<ResourcesService>();
            services.AddSingleton<ThemeService>();

            var viewModelsTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x =>
                    x.GetCustomAttribute<ViewModelAttribute>() != null
                );

            foreach (var type in viewModelsTypes)
                services.AddTransient(type);

            services.AddLogging(x => x.AddConsole());
            services.AddDataAccess(File.ReadAllText("connectionstring.txt"));
            services.AddDomain();
            services.AddApplication();
            services.AddApplicationVersion1_0();
            services.AddDirectClient();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
