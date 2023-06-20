﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    internal sealed partial class DriversViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private bool _showOnlyActive;

        public DriversViewModel(ApiService apiService)
        {
            _apiService = apiService;
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
                    $"{x.LastName} {x.FirstName} {x.Patronymic}",
                    x.State.ToString(),
                    x.TariffName,
                    null
                ))
                .ToArray()
            );

            LoadingState = LoadingState.Loaded;
        }

        partial void OnFilterChanged(string value)
        {
            Drivers.Filter = x => x.FullName.Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
