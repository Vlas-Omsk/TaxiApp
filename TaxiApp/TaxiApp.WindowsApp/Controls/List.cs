using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace TaxiApp.WindowsApp.Controls
{
    public class List : Control
    {
        static List()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(List), new FrameworkPropertyMetadata(typeof(List)));
        }

        #region SearchPlaceholderProperty

        public string SearchPlaceholder
        {
            get => (string)GetValue(SearchPlaceholderProperty);
            set => SetValue(SearchPlaceholderProperty, value);
        }

        public readonly static DependencyProperty SearchPlaceholderProperty = DependencyProperty.Register(
            nameof(SearchPlaceholder),
            typeof(string),
            typeof(List),
            new PropertyMetadata()
        );

        #endregion

        #region SearchTextProperty

        public string SearchText
        {
            get => (string)GetValue(SearchTextProperty);
            set => SetValue(SearchTextProperty, value);
        }

        public readonly static DependencyProperty SearchTextProperty = DependencyProperty.Register(
            nameof(SearchText),
            typeof(string),
            typeof(List),
            new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
            }
        );

        #endregion

        #region ItemsSourceProperty

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public readonly static DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(List),
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
            typeof(List),
            new PropertyMetadata()
        );

        #endregion

        #region ItemInfoTemplateProperty

        public DataTemplate ItemInfoTemplate
        {
            get => (DataTemplate)GetValue(ItemInfoTemplateProperty);
            set => SetValue(ItemInfoTemplateProperty, value);
        }

        public readonly static DependencyProperty ItemInfoTemplateProperty = DependencyProperty.Register(
            nameof(ItemInfoTemplate),
            typeof(DataTemplate),
            typeof(List),
            new PropertyMetadata()
        );

        #endregion

        #region TitleTemplateProperty

        public DataTemplate TitleTemplate
        {
            get => (DataTemplate)GetValue(TitleTemplateProperty);
            set => SetValue(TitleTemplateProperty, value);
        }

        public readonly static DependencyProperty TitleTemplateProperty = DependencyProperty.Register(
            nameof(TitleTemplate),
            typeof(DataTemplate),
            typeof(List),
            new PropertyMetadata()
        );

        #endregion
    }
}
