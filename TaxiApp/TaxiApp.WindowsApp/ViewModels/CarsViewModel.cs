using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System.Linq;
using System.Threading.Tasks;
using TaxiApp.Application.Version1.Queries;
using TaxiApp.WindowsApp.Models;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class CarsViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        public CarsViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [ObservableProperty]
        private CarModel[] _cars;

        [ObservableProperty]
        private string _loadingStatus;

        [ObservableProperty]
        private LoadingState _loadingState;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new GetCarsQuery());

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Cars = response.Value
                .Select(x => new CarModel(
                    x.Number,
                    x.DriverFullName,
                    x.Brand,
                    x.Color,
                    null
                ))
                .ToArray();

            LoadingState = LoadingState.Loaded;
        }
    }
}
