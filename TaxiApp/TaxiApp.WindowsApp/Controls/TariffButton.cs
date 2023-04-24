using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaxiApp.WindowsApp.Controls
{
    public sealed class TariffButton : Control
    {
        static TariffButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TariffButton), new FrameworkPropertyMetadata(typeof(TariffButton)));
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
            typeof(TariffButton)
        );

        #endregion

        #region ImageTemplateProperty

        public DataTemplate ImageTemplate
        {
            get => (DataTemplate)GetValue(ImageTemplateProperty);
            set => SetValue(ImageTemplateProperty, value);
        }

        public readonly static DependencyProperty ImageTemplateProperty = DependencyProperty.Register(
            nameof(ImageTemplate),
            typeof(DataTemplate),
            typeof(TariffButton),
            new PropertyMetadata()
        );

        #endregion

        #region CommandProperty

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public readonly static DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(TariffButton),
            new PropertyMetadata()
        );

        #endregion
    }
}
