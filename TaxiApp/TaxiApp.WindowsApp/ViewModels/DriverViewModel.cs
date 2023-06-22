using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class DriverViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;
        private int _id;

        public DriverViewModel(
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
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string _lastName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string _firstName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FullName))]
        private string _patronymic;

        public string FullName => $"{LastName} {FirstName} {Patronymic}";

        [ObservableProperty]
        private string _inn;

        [ObservableProperty]
        private string _passport;

        [ObservableProperty]
        private string _address;

        [ObservableProperty]
        private string _additionalInfo;

        [ObservableProperty]
        private LoadingState _loadingState;

        [ObservableProperty]
        private string _loadingStatus;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new GetDriverInfoQuery(_id));

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            LastName = response.Value.FullName.LastName;
            FirstName = response.Value.FullName.FirstName;
            Patronymic = response.Value.FullName.Patronymic;
            Inn = response.Value.Inn;
            Passport = response.Value.Passport;
            Address = response.Value.Address;
            AdditionalInfo = response.Value.AdditionalInfo;

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

            var response = await _apiService.Send(new UpdateDriverCommand(
                _id,
                new FullName(
                    LastName,
                    FirstName,
                    Patronymic
                ),
                Inn,
                Passport,
                Address,
                AdditionalInfo,
                null
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

            var response = await _apiService.Send(new DeleteDriverCommand(_id));

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

        [RelayCommand]
        private void OpenReport()
        {
            _navigationService.NavigateTo(new DriverReportView(_id));
        }
    }
}
