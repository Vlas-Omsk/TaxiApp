using System;
using System.Windows;

namespace TaxiApp.WindowsApp.Services
{
    internal sealed class ThemeService
    {
        private ResourceDictionary _resourceDictionary;

        public void SetTheme(Theme theme)
        {
            if (_resourceDictionary != null)
                App.Current.Resources.MergedDictionaries.Remove(_resourceDictionary);

            _resourceDictionary = theme switch
            {
                Theme.White => new ResourceDictionary()
                {
                    Source = new Uri("/Resources/ColorsWhite.xaml", UriKind.RelativeOrAbsolute)
                },
                Theme.Black => new ResourceDictionary()
                {
                    Source = new Uri("/Resources/ColorsBlack.xaml", UriKind.RelativeOrAbsolute)
                },
                _ => throw new NotSupportedException()
            };

            App.Current.Resources.MergedDictionaries.Add(_resourceDictionary);
        }
    }
}
