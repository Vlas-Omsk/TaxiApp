using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System.Threading.Tasks;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.WindowsApp.Models;
using TaxiApp.WindowsApp.Services;
using System;
using System.Linq;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class UsersViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;

        public UsersViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private FilteredCollection<UserModel> _users;

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

            var response = await _apiService.Send(new GetUsersQuery());

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Users = new FilteredCollection<UserModel>(
                response.Value
                    .Select(x => new UserModel(
                        x.Login,
                        x.FullName,
                        x.Role.ToString()
                    ))
                    .ToArray()
            );

            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Open(UserModel user)
        {
            _navigationService.NavigateTo(new UserView(user.Login));
        }

        partial void OnFilterChanged(string value)
        {
            Users.Filter = x => x.FullName.ToString().Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
