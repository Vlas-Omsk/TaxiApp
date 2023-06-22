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

        #region HeaderContentProperty

        public object HeaderContent
        {
            get => (object)GetValue(HeaderContentProperty);
            set => SetValue(HeaderContentProperty, value);
        }

        public readonly static DependencyProperty HeaderContentProperty = DependencyProperty.Register(
            nameof(HeaderContent),
            typeof(object),
            typeof(PageTemplate)
        );

        #endregion
    }
}
