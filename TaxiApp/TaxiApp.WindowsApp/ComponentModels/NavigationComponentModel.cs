using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.ComponentModels
{
    [ViewModel]
    internal sealed partial class NavigationComponentModel : ObservableObject
    {
        private readonly NavigationService _navigationService;

        public NavigationComponentModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private void Back()
        {
            _navigationService.NavigateBack();
        }
    }
}
