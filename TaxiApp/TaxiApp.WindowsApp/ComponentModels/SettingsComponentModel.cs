using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TaxiApp.WindowsApp.ComponentModels
{
    [ViewModel]
    internal sealed partial class SettingsComponentModel : ObservableObject
    {
        [RelayCommand]
        private void OpenSettings()
        {
        }

        [RelayCommand]
        private void OpenHelp()
        {
        }

        [RelayCommand]
        private void Exit()
        {
            App.Current.Shutdown();
        }
    }
}
