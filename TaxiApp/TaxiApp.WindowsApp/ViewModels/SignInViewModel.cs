using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using TaxiApp.DataTypes;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class SignInViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;
        private readonly SessionService _sessionService;

        public SignInViewModel(NavigationService navigationService, SessionService sessionService)
        {
            _navigationService = navigationService;
            _sessionService = sessionService;
        }

        [ObservableProperty]
        private string _login;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _error;

        [RelayCommand]
        private void Continue()
        {
            if (!Enum.TryParse<UserRole>(Login, true, out var userRole))
            {
                Error = $"Only {string.Join(", ", Enum.GetNames<UserRole>().Select(x => $"'{x}'"))} awailable as login";

                return;
            }

            _sessionService.SignIn(new User(userRole));

            _navigationService.NavigateTo(new MenuView());
        }

        [RelayCommand]
        private void Create()
        {
            Error = "'Create' not implemented";
        }
    }
}
