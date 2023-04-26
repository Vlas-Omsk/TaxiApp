using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using TaxiApp.DAL.Abstractions;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class StartupViewModel : ObservableObject
    {
        private readonly NavigationService _navigationService;
        private readonly ApiService _apiService;
        private readonly IApplicationDbContext _applicationDbContext;

        public StartupViewModel(
            NavigationService navigationService,
            ApiService apiService,
            IApplicationDbContext applicationDbContext
        )
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _applicationDbContext = applicationDbContext;
        }

        [ObservableProperty]
        private string _loadingStatus;

        [RelayCommand]
        private async Task Load()
        {
            LoadingStatus = "Running migrations on database";

            await _applicationDbContext.Migrate();

            LoadingStatus = "Connecting to api";

            await _apiService.Connect();

            LoadingStatus = null;

#if RELEASE
            await Task.Delay(2000);
#endif

            _navigationService.NavigateTo(new SignInView());
        }
    }
}
