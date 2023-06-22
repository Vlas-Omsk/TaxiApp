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
    internal sealed partial class OrdersViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;

        public OrdersViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private FilteredCollection<OrderModel> _orders;

        [ObservableProperty]
        private LoadingState _loadingState;

        [ObservableProperty]
        private string _loadingStatus;

        [ObservableProperty]
        private string _filter;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new GetOrdersQuery());

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Orders = new FilteredCollection<OrderModel>(response.Value
                .Select(x => new OrderModel(
                    x.Id,
                    x.CreatedAt.ToLocalTime().ToString("D"),
                    x.CreatedAt.ToLocalTime().ToString("T"),
                    x.DriverFullName?.ToString()
                ))
                .ToArray()
            );

            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Open(OrderModel order)
        {
            _navigationService.NavigateTo(new OrderView(EditType.View, order.Id));
        }

        [RelayCommand]
        private void Add()
        {
            _navigationService.NavigateTo(new OrderView(EditType.Create));
        }

        partial void OnFilterChanged(string value)
        {
            Orders.Filter = x => x.Id.ToString().Contains(value, StringComparison.OrdinalIgnoreCase);
        }
    }
}
