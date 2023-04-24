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

        public StartupViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task Load()
        {
#if RELEASE
            await Task.Delay(2000);
#endif

            _navigationService.NavigateTo(new SignInView());
        }
    }
}
