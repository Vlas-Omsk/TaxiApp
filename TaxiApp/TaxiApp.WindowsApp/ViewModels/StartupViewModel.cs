using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using TaxiApp.Client.Abstractions;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class StartupViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;
        private readonly IClient _client;

        public StartupViewModel(NavigationService navigationService, IClient client)
        {
            _navigationService = navigationService;
            _client = client;
        }

        [RelayCommand]
        private async Task Load()
        {
            await _client.Connect();

#if RELEASE
            await Task.Delay(2000);
#endif

            _navigationService.NavigateTo(new SignInView());
        }
    }
}
