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
    internal sealed partial class DriversViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;
        private bool _showOnlyActive;

        public DriversViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        public void Init(bool showOnlyActive)
        {
            _showOnlyActive = showOnlyActive;
        }

        [ObservableProperty]
        private FilteredCollection<DriverModel> _drivers;

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

            var response = await _apiService.Send(new GetDriversQuery()
            {
                FilterActive = _showOnlyActive
            });

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Drivers = new FilteredCollection<DriverModel>(response.Value
                .Select(x => new DriverModel(
                    x.Id,
                    $"{x.FullName.LastName} {x.FullName.FirstName} {x.FullName.Patronymic}",
                    x.State.ToString(),
                    x.TariffName,
                    x.AdditionalInfo
                ))
                .ToArray()
            );

            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Open(DriverModel driver)
        {
            _navigationService.NavigateTo(new DriverView(driver.Id));
        }

        partial void OnFilterChanged(string value)
        {
            Drivers.Filter = x => x.FullName.Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
