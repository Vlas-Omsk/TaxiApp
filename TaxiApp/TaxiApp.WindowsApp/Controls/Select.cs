using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    public class Select : Control
    {
        static Select()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Select), new FrameworkPropertyMetadata(typeof(Select)));
        }

        #region LabelProperty

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public readonly static DependencyProperty LabelProperty = DependencyProperty.Register(
            nameof(Label),
            typeof(string),
            typeof(Select),
            new PropertyMetadata()
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
            typeof(Select),
            new PropertyMetadata()
        );

        #endregion

        #region SelectedValueProperty

        public object SelectedValue
        {
            get => GetValue(SelectedValueProperty);
            set => SetValue(SelectedValueProperty, value);
        }

        public readonly static DependencyProperty SelectedValueProperty = DependencyProperty.Register(
            nameof(SelectedValue),
            typeof(object),
            typeof(Select),
            new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
            }
        );

        #endregion

        #region DisplayPathProperty

        public string DisplayPath
        {
            get => (string)GetValue(DisplayPathProperty);
            set => SetValue(DisplayPathProperty, value);
        }

        public readonly static DependencyProperty DisplayPathProperty = DependencyProperty.Register(
            nameof(DisplayPath),
            typeof(string),
            typeof(Select),
            new PropertyMetadata()
        );

        #endregion

        #region IsReadOnlyProperty

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        public readonly static DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            nameof(IsReadOnly),
            typeof(bool),
            typeof(Select),
            new PropertyMetadata()
        );

        #endregion
    }
}
