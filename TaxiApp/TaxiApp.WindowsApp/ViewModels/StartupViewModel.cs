using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using TaxiApp.DAL.Abstractions;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class StartupViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;
        private readonly ApiService _apiService;
        private readonly IMigrationsRunner _migrationsRunner;

        public StartupViewModel(
            NavigationService navigationService,
            ApiService apiService,
            IMigrationsRunner migrationsRunner
        )
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _migrationsRunner = migrationsRunner;
        }

        [ObservableProperty]
        private string _loadingStatus;

        [RelayCommand]
        private async Task Load()
        {
            LoadingStatus = "Running migrations on database";

            await _migrationsRunner.Run();

            LoadingStatus = "Connecting to api";

            await _apiService.Connect();

            LoadingStatus = null;

#if RELEASE
            await Task.Delay(2000);
#endif

            _navigationService.NavigateTo(new SignInView());
        }
    }
}
