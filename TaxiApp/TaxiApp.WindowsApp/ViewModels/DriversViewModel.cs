using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using TaxiApp.WindowsApp.Models;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class DriversViewModel : ObservableObject
    {
        private bool _showActive;

        public void Init(bool showOnlyActive)
        {
            _showActive = showOnlyActive;
        }

        [ObservableProperty]
        private DriverModel[] _drivers;

        [ObservableProperty]
        private LoadingState _loadingState;

        [RelayCommand]
        private void Load()
        {
            var drivers = new DriverModel[10];

            for (var i = 0; i < 10; i++)
            {
                drivers[i] = new DriverModel(
                    "full name " + i,
                    "status " + i,
                    "tariff " + i,
                    "additional info " + i
                );
            }

            Drivers = drivers;

            LoadingState = LoadingState.Loaded;
        }
    }
}
