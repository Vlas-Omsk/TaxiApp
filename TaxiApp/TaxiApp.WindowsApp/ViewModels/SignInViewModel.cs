using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Application.Version1_0.Queries;
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
        private readonly ApiService _apiService;

        public SignInViewModel(
            NavigationService navigationService,
            SessionService sessionService,
            ApiService apiService
        )
        {
            _navigationService = navigationService;
            _sessionService = sessionService;
            _apiService = apiService;
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
            await _apiService.SetAuthorization(Login, Password);

            var result = await _apiService.Send(new GetCurrentUserQuery());

            if (!result.Success)
            {
                await _apiService.SetAuthorization(null, null);

                Error = result.Message;
                return;
            }

            _sessionService.SignIn(new User(result.Value.Role));

            _navigationService.NavigateTo(new MenuView());
        }

        [RelayCommand]
        private async Task Create()
        {
            var result = await _apiService.Send(new CreateUserCommand(
                Login,
                Password,
                UserRole.Administrator
            ));

            if (!result.Success)
            {
                Error = result.Message;
                return;
            }

            Error = "Successfully created account";
        }
    }
}
