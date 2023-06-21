using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class TariffViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;
        private int _id;

        public TariffViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        public void Init(int id)
        {
            _id = id;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EditButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(SaveButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(EditPhotoButtonVisibility))]
        private bool _isReadOnly = true;

        public Visibility EditButtonVisibility => IsReadOnly ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SaveButtonVisibility => IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
        public Visibility EditPhotoButtonVisibility => IsReadOnly ? Visibility.Hidden : Visibility.Visible;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private decimal _startingPrice;

        [ObservableProperty]
        private TimeSpan _freeWaiting;

        [ObservableProperty]
        private decimal _paidWaitingPricePerMin;

        [ObservableProperty]
        private decimal _inCityPricePerKm;

        [ObservableProperty]
        private decimal _outsideCityPricePerKm;

        [ObservableProperty]
        private decimal _waitingOnWayPricePerMin;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private LoadingState _loadingState;

        [ObservableProperty]
        private string _loadingStatus;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new GetTariffInfoQuery(_id));

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Name = response.Value.Name;
            StartingPrice = response.Value.StartingPrice;
            FreeWaiting = response.Value.FreeWaiting;
            PaidWaitingPricePerMin = response.Value.PaidWaitingPricePerMin;
            InCityPricePerKm = response.Value.InCityPricePerKm;
            OutsideCityPricePerKm = response.Value.OutsideCityPricePerKm;
            WaitingOnWayPricePerMin = response.Value.WaitingOnWayPricePerMin;
            Description = response.Value.Description;

            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Edit()
        {
            IsReadOnly = false;
        }

        [RelayCommand]
        private async Task Save()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new UpdateTariffCommand(
                _id,
                Name,
                StartingPrice,
                FreeWaiting,
                PaidWaitingPricePerMin,
                InCityPricePerKm,
                OutsideCityPricePerKm,
                WaitingOnWayPricePerMin,
                Description
            ));

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            IsReadOnly = true;
            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private async Task Delete()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new DeleteTariffCommand(_id));

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            LoadingState = LoadingState.Loaded;
            _navigationService.NavigateBack();
        }
    }
}
