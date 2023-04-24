using CommunityToolkit.Mvvm.ComponentModel;
using System;
using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.WindowModels
{
    [ViewModel]
    internal sealed partial class MainWindowModel : ObservableObject
    {
        private readonly NavigationService _navigationService;

        public MainWindowModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            UpdateCurrentView();

            _navigationService.CurrentViewChanged += OnNavigationServiceCurrentViewChanged;
        }

        ~MainWindowModel()
        {
            _navigationService.CurrentViewChanged -= OnNavigationServiceCurrentViewChanged;
        }

        [ObservableProperty]
        private View _currentView;

        private void OnNavigationServiceCurrentViewChanged(object sender, EventArgs e)
        {
            UpdateCurrentView();
        }

        private void UpdateCurrentView()
        {
            CurrentView = _navigationService.CurrentView;
        }
    }
}
