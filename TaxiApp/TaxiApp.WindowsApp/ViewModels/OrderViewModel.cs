using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.WindowsApp.Images;
using TaxiApp.WindowsApp.Models;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class OrderViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private readonly NavigationService _navigationService;
        private int _id;
        private DateTime _createdAt;

        public OrderViewModel(
            ApiService apiService,
            NavigationService navigationService
        )
        {
            _apiService = apiService;
            _navigationService = navigationService;
        }

        public void Init(EditType editType, int? id = null)
        {
            EditType = editType;
            IdInternal = id.GetValueOrDefault();
        }

        private int IdInternal
        {
            get => _id;
            set
            {
                _id = value;

                OnPropertyChanged(nameof(Id));
            }
        }

        private DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;

                OnPropertyChanged(nameof(CreatedAtText));
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EditButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(SaveButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(EditPhotoButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(Id))]
        [NotifyPropertyChangedFor(nameof(CreatedAtText))]
        private EditType _editType;

        public bool IsReadOnly => EditType == EditType.View;
        public Visibility EditButtonVisibility => IsReadOnly ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SaveButtonVisibility => IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
        public Visibility EditPhotoButtonVisibility => IsReadOnly ? Visibility.Hidden : Visibility.Visible;

        public string Id => EditType != EditType.Create ? IdInternal.ToString() : "#";
        public string CreatedAtText => EditType != EditType.Create ? CreatedAt.ToString() : "-";

        [ObservableProperty]
        private decimal _cost;

        [ObservableProperty]
        private string _addressFrom;

        [ObservableProperty]
        private string _addressTo;

        [ObservableProperty]
        private string _comment;

        [ObservableProperty]
        private OrderDriverModel[] _drivers;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsTariffReadOnly))]
        private OrderDriverModel _driver;

        [ObservableProperty]
        private OrderTariffModel _tariff;

        [ObservableProperty]
        private OrderClientModel[] _clients;

        [ObservableProperty]
        private OrderClientModel _client;

        [ObservableProperty]
        private LoadingState _loadingState;

        [ObservableProperty]
        private string _loadingStatus;

        public bool IsTariffReadOnly => Driver == null;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;

            var driversResponse = await _apiService.Send(new GetOrderDriversQuery());

            if (!driversResponse.Success)
            {
                LoadingStatus = driversResponse.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            var clientsResponse = await _apiService.Send(new GetOrderClientsQuery());

            if (!clientsResponse.Success)
            {
                LoadingStatus = clientsResponse.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Drivers = driversResponse.Value
                .Select(x => new OrderDriverModel(
                    x.Id,
                    x.FullName,
                    x.Tariffs
                        .Select(x => new OrderTariffModel(
                            x.Id,
                            x.Name
                        ))
                        .ToArray(),
                    new OrderCarModel(
                        x.Car.Id,
                        x.Car.Number
                    )
                ))
                .ToArray();

            Clients = clientsResponse.Value
                .Select(x => new OrderClientModel(
                    x.Id,
                    x.Name
                ))
                .ToArray();

            if (EditType is EditType.Edit or EditType.View)
            {
                var response = await _apiService.Send(new GetOrderInfoQuery(IdInternal));

                if (!response.Success)
                {
                    LoadingStatus = response.Message;
                    LoadingState = LoadingState.Failed;
                    return;
                }

                Driver = Drivers.First(x => x.Id == response.Value.DriverId);
                Cost = response.Value.Cost;
                AddressFrom = response.Value.AddressFrom;
                AddressTo = response.Value.AddressTo;
                CreatedAt = response.Value.CreatedAt;
                Comment = response.Value.Comment;
            }

            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Edit()
        {
            EditType = EditType.Edit;
        }

        [RelayCommand]
        private async Task Save()
        {
            if (Driver == null)
            {
                MessageBox.Show("Водитель не выбран");
                return;
            }

            if (Client == null)
            {
                MessageBox.Show("Клиент не выбран");
                return;
            }

            LoadingState = LoadingState.Loading;

            if (EditType == EditType.Create)
            {
                var response = await _apiService.Send(new CreateOrderCommand(
                    Driver.Id,
                    Client.Id,
                    Cost,
                    AddressFrom,
                    AddressTo,
                    Comment
                ));

                if (!response.Success)
                {
                    LoadingStatus = response.Message;
                    LoadingState = LoadingState.Failed;
                    return;
                }
            }
            else if (EditType == EditType.Edit)
            {
                var response = await _apiService.Send(new UpdateOrderCommand(
                    IdInternal,
                    Driver.Id,
                    Client.Id,
                    Cost,
                    AddressFrom,
                    AddressTo,
                    Comment
                ));

                if (!response.Success)
                {
                    LoadingStatus = response.Message;
                    LoadingState = LoadingState.Failed;
                    return;
                }
            }
            else
            {
                throw new Exception();
            }

            EditType = EditType.View;
            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private async Task Delete()
        {
            LoadingState = LoadingState.Loading;

            if (EditType is EditType.Edit or EditType.View)
            {
                var response = await _apiService.Send(new DeleteOrderCommand(_id));

                if (!response.Success)
                {
                    LoadingStatus = response.Message;
                    LoadingState = LoadingState.Failed;
                    return;
                }
            }

            LoadingState = LoadingState.Loaded;
            _navigationService.NavigateBack();
        }
    }
}
