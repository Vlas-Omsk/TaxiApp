using System.Windows;
using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    public class Field : Control
    {
        static Field()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Field), new FrameworkPropertyMetadata(typeof(Field)));
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
            typeof(Field),
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
            typeof(Field),
            new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
            }
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
            typeof(Field),
            new PropertyMetadata()
        );

        #endregion
    }
}
