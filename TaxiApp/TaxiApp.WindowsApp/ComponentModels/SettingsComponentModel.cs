using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaxiApp.WindowsApp.Services;
using TaxiApp.WindowsApp.Views;

namespace TaxiApp.WindowsApp.ComponentModels
{
    [ViewModel]
    internal sealed partial class SettingsComponentModel : ObservableObject
    {
        private readonly ThemeService _themeService;
        private readonly ResourcesService _resourcesService;
        private readonly NavigationService _navigationService;

        public SettingsComponentModel(
            ThemeService themeService,
            ResourcesService resourcesService,
            NavigationService navigationService
        )
        {
            _themeService = themeService;
            _resourcesService = resourcesService;
            _navigationService = navigationService;

            _themeService.ThemeChanged += OnThemeServiceThemeChanged;

            UpdateIsWhiteTheme();
            UpdateThemeText();
        }

        ~SettingsComponentModel()
        {
            _themeService.ThemeChanged -= OnThemeServiceThemeChanged;
        }

        [ObservableProperty]
        private bool _isWhiteTheme;

        [ObservableProperty]
        private string _themeText;

        [ObservableProperty]
        private bool _isSettingsOpened;

        [RelayCommand]
        private void OpenSettings()
        {
            IsSettingsOpened = !IsSettingsOpened;
        }

        [RelayCommand]
        private void OpenHelp()
        {
            _navigationService.NavigateTo(new HelpView());
        }

        [RelayCommand]
        private void Exit()
        {
            App.Current.Shutdown();
        }

        private void UpdateIsWhiteTheme()
        {
            IsWhiteTheme = _themeService.Theme == Theme.White;
        }

        private void UpdateThemeText()
        {
            ThemeText = IsWhiteTheme ?
                _resourcesService.FindString("TurnOffLight") :
                _resourcesService.FindString("TurnOnLight");
        }

        partial void OnIsWhiteThemeChanged(bool value)
        {
            _themeService.SetTheme(value ? Theme.White : Theme.Black);

            UpdateThemeText();
        }

        private void OnThemeServiceThemeChanged(object sender, System.EventArgs e)
        {
            UpdateIsWhiteTheme();
        }
    }
}
