using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.WindowsApp.Models;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class CarViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;
        private int _id;

        public CarViewModel(
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
        private string _number;

        [ObservableProperty]
        private string _brand;

        [ObservableProperty]
        private string _model;

        [ObservableProperty]
        private string _color;

        [ObservableProperty]
        private string _additionalInfo;

        [ObservableProperty]
        private TariffModel[] _tariffs;

        [ObservableProperty]
        private int _yearOfManufacture;

        [ObservableProperty]
        private LoadingState _loadingState;

        [ObservableProperty]
        private string _loadingStatus;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new GetCarInfoQuery(_id));

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Number = response.Value.Number;
            Brand = response.Value.Brand;
            Model = response.Value.Model;
            Color = response.Value.Color;
            AdditionalInfo = response.Value.AdditionalInfo;
            Tariffs = response.Value.Tariffs
                .Select(x => new TariffModel(x.Id, x.Name))
                .ToArray();
            YearOfManufacture = response.Value.YearOfManufacture;

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

            var response = await _apiService.Send(new UpdateCarCommand(
                _id,
                Number,
                Color,
                Tariffs.Select(x => x.Id).ToArray(),
                YearOfManufacture,
                Brand,
                Model,
                AdditionalInfo
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

            var response = await _apiService.Send(new DeleteCarCommand(_id));

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            LoadingState = LoadingState.Loaded;
            _navigationService.NavigateBack();
        }

        [RelayCommand]
        private void EditPhoto()
        {

        }
    }
}
