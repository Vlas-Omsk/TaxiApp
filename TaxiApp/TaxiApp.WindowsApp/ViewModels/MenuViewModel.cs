using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using TaxiApp.DataTypes;
using TaxiApp.WindowsApp.Models;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class MenuViewModel : ObservableObject
    {
        private readonly SessionService _sessionService;
        private readonly ResourcesService _resourcesService;
        private readonly NavigationService _navigationService;

        public MenuViewModel(
            SessionService sessionService,
            ResourcesService resourcesService,
            NavigationService navigationService
        )
        {
            _sessionService = sessionService;
            _resourcesService = resourcesService;
            _navigationService = navigationService;

            var items = new List<MenuItemModel>();

            switch (sessionService.User.Role)
            {
                case UserRole.Director:
                    items.Add(new MenuItemModel(
                        null,
                        resourcesService.FindString("Reports"),
                        null
                    ));
                    break;
                case UserRole.Administrator:
                    items.Add(new MenuItemModel(
                        new Images.Car(),
                        resourcesService.FindString("Cars"),
                        OpenCarsCommand
                    ));
                    items.Add(new MenuItemModel(
                        new Images.Tariff(),
                        resourcesService.FindString("Tariffs"),
                        OpenTariffsCommand
                    ));
                    items.Add(new MenuItemModel(
                        new Images.Driver(),
                        resourcesService.FindString("Drivers"),
                        OpenDriversCommand
                    ));
                    items.Add(new MenuItemModel(
                        null,
                        resourcesService.FindString("Report"),
                        null
                    ));
                    items.Add(new MenuItemModel(
                        new Images.Report(),
                        resourcesService.FindString("Users"),
                        OpenUsersCommand
                    ));
                    break;
                case UserRole.Dispatcher:
                    items.Add(new MenuItemModel(
                        new Images.Driver(),
                        resourcesService.FindString("ActiveDrivers"),
                        OpenActiveDriversCommand
                    ));
                    items.Add(new MenuItemModel(
                        null,
                        resourcesService.FindString("Report"),
                        null
                    ));
                    break;
            }

            if (sessionService.User.Role is UserRole.Dispatcher or UserRole.Administrator)
            {
                items.Add(new MenuItemModel(
                    new Images.Order(),
                    resourcesService.FindString("Orders"),
                    OpenOrdersCommand
                ));
                items.Add(new MenuItemModel(
                    null,
                    resourcesService.FindString("Clients"),
                    null
                ));
            }

            Items = items.ToArray();
        }

        [ObservableProperty]
        private MenuItemModel[] _items;

        [RelayCommand]
        private void OpenCars()
        {
            _navigationService.NavigateTo(new CarsView());
        }

        [RelayCommand]
        private void OpenTariffs()
        {
            _navigationService.NavigateTo(new TariffsView());
        }

        [RelayCommand]
        private void OpenDrivers()
        {
            _navigationService.NavigateTo(new DriversView(showOnlyActive: false));
        }

        [RelayCommand]
        private void OpenOrders()
        {
            _navigationService.NavigateTo(new OrdersView());
        }

        [RelayCommand]
        private void OpenActiveDrivers()
        {
            _navigationService.NavigateTo(new DriversView(showOnlyActive: true));
        }

        [RelayCommand]
        private void OpenUsers()
        {
            _navigationService.NavigateTo(new UsersView());
        }
    }
}
