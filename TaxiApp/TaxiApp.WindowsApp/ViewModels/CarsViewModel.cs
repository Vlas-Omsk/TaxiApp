using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.WindowsApp.Models;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class CarsViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;

        public CarsViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
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
                        x.Id,
                        x.Number,
                        x.DriverFullName,
                        x.Brand,
                        x.Color,
                        x.AdditionalInfo
                    ))
                    .ToArray()
            );

            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Open(CarModel car)
        {
            _navigationService.NavigateTo(new CarView(car.Id));
        }

        partial void OnFilterChanged(string value)
        {
            Cars.Filter = x => x.GovermentNumber.Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
