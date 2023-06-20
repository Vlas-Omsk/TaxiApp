using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System;
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
        private FilteredCollection<CarModel> _cars;

        [ObservableProperty]
        private string _loadingStatus;

        [ObservableProperty]
        private LoadingState _loadingState;

        [ObservableProperty]
        private string _filter;

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

            Cars = new FilteredCollection<CarModel>(
                response.Value
                    .Select(x => new CarModel(
                        x.Number,
                        x.DriverFullName,
                        x.Brand,
                        x.Color,
                        null
                    ))
                    .ToArray()
            );

            LoadingState = LoadingState.Loaded;
        }

        partial void OnFilterChanged(string value)
        {
            Cars.Filter = x => x.GovermentNumber.Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
