using System;
using System.Windows;

namespace TaxiApp.WindowsApp.Services
{
    internal sealed class ThemeService
    {
        private ResourceDictionary _resourceDictionary;

        public Theme? Theme { get; private set; }

        public event EventHandler ThemeChanged;

        public void SetTheme(Theme theme)
        {
            if (Theme == theme)
                return;

            Theme = theme;

            if (_resourceDictionary != null)
                App.Current.Resources.MergedDictionaries.Remove(_resourceDictionary);

            _resourceDictionary = theme switch
            {
                WindowsApp.Theme.White => new ResourceDictionary()
                {
                    Source = new Uri("/Resources/ColorsWhite.xaml", UriKind.RelativeOrAbsolute)
                },
                WindowsApp.Theme.Black => new ResourceDictionary()
                {
                    Source = new Uri("/Resources/ColorsBlack.xaml", UriKind.RelativeOrAbsolute)
                },
                _ => throw new NotSupportedException()
            };

            App.Current.Resources.MergedDictionaries.Add(_resourceDictionary);

            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
