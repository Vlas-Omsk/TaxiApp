using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class UserViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;

        public UserViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        public void Init(string login)
        {
            Login = login;
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
        private string _login;

        [ObservableProperty]
        private string _password;

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
        private UserRole _role;

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

            var response = await _apiService.Send(new GetUserInfoQuery(_login));

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
            Role = response.Value.Role;
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

            var response = await _apiService.Send(new UpdateUserCommand(
                Login,
                Password,
                new FullName(
                    LastName,
                    FirstName,
                    Patronymic
                ),
                Inn,
                Passport,
                Address,
                Role,
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

            var response = await _apiService.Send(new DeleteUserCommand(Login));

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
