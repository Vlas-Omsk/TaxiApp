using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class OrdersViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoadingState _loadingState;

        [RelayCommand]
        private void Load()
        {
            LoadingState = LoadingState.Loaded;
        }
    }
}
