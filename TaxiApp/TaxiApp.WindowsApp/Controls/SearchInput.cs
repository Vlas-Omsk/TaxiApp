using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaxiApp.WindowsApp.Controls
{
    public class SearchInput : Control
    {
        static SearchInput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchInput), new FrameworkPropertyMetadata(typeof(SearchInput)));
        }

        #region CommandProperty

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public readonly static DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(SearchInput),
            new PropertyMetadata()
        );

        #endregion

        #region PlaceholderProperty

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public readonly static DependencyProperty PlaceholderProperty = DependencyProperty.Register(
            nameof(Placeholder),
            typeof(string),
            typeof(SearchInput),
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
            typeof(SearchInput),
            new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
            }
        );

        #endregion
    }
}
