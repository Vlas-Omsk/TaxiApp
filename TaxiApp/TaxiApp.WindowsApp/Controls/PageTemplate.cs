using System.Windows;
using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    public sealed class PageTemplate : ContentControl
    {
        static PageTemplate()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageTemplate), new FrameworkPropertyMetadata(typeof(PageTemplate)));
        }

        #region TitleProperty

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public readonly static DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(PageTemplate)
        );

        #endregion
    }
}
