using System.Windows;
using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    public class Input : Control
    {
        static Input()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Input), new FrameworkPropertyMetadata(typeof(Input)));
        }

        #region PlaceholderProperty

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public readonly static DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            nameof(Placeholder),
            typeof(string),
            typeof(Input),
            new PropertyMetadata()
        );

        #endregion

        #region TextProperty

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public readonly static DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(Input),
            new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
            }
        );

        #endregion
    }
}
