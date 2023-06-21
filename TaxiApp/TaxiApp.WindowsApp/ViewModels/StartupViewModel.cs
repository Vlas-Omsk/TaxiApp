using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class StartupViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;
        private readonly ApiService _apiService;

        public StartupViewModel(
            NavigationService navigationService,
            ApiService apiService
        )
        {
            _navigationService = navigationService;
            _apiService = apiService;
        }

        [ObservableProperty]
        private string _loadingStatus;

        [RelayCommand]
        private async Task Load()
        {
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
