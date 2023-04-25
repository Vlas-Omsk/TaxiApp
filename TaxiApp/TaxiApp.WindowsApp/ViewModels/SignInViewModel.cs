using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaxiApp.Application.Version1.Commands;
using TaxiApp.Application.Version1.Queries;
using TaxiApp.Client.Abstractions;
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
        private readonly IClient _client;

        public SignInViewModel(NavigationService navigationService, SessionService sessionService, IClient client)
        {
            _navigationService = navigationService;
            _sessionService = sessionService;
            _client = client;
        }

        [ObservableProperty]
        private string _login;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _error;

        [RelayCommand]
        private async Task Continue()
        {
            await _client.SetAuthorization(Login, Password);

            var result = await _client.Send(new GetCurrentUserQuery());

            if (!result.Success)
            {
                Error = $"{result.Error.Type}";
                return;
            }

            _sessionService.SignIn(new User(result.Value.Role));

            _navigationService.NavigateTo(new MenuView());
        }

        [RelayCommand]
        private async Task Create()
        {
            var result = await _client.Send(new CreateUserCommand(
                Login,
                Password,
                UserRole.Administrator
            ));

            if (!result.Success)
                Error = $"{result.Error.Type}";
        }
    }
}
