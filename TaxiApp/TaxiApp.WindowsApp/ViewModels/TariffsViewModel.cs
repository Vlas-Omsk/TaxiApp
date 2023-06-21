using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System.Linq;
using System.Threading.Tasks;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.WindowsApp.Models;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class TariffsViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;

        public TariffsViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private TariffModel[] _tariffs;

        [ObservableProperty]
        private string _loadingStatus;

        [ObservableProperty]
        private LoadingState _loadingState;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new GetTariffsQuery());

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Tariffs = response.Value
                .Select(x => new TariffModel(
                    x.Id,
                    x.Name
                ))
                .ToArray();

            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Open(TariffModel tariff)
        {
            _navigationService.NavigateTo(new TariffView(tariff.Id));
        }
    }
}
