using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using TaxiApp.WindowsApp.Models;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class CarsViewModel : ObservableObject
    {
        [ObservableProperty]
        private CarModel[] _cars;

        [ObservableProperty]
        private LoadingState _loadingState;

        [RelayCommand]
        private void Load()
        {
            var cars = new CarModel[10];

            for (var i = 0; i < 10; i++)
            {
                cars[i] = new CarModel(
                    "goverment number " + i,
                    "driver full name " + i,
                    "model " + i,
                    "color " + i,
                    "additional info " + i
                );
            }

            Cars = cars;

            LoadingState = LoadingState.Loaded;
        }
    }
}
