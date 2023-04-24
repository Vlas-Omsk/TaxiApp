using System.Windows;
using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    [TemplatePart(Name = "PART_Input", Type = typeof(TextBox))]
    public class PlaceholderInput : Control
    {
        private TextBox _input;

        static PlaceholderInput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderInput), new FrameworkPropertyMetadata(typeof(PlaceholderInput)));
        }

        public override void OnApplyTemplate()
        {
            _input = (TextBox)GetTemplateChild("PART_Input");

            _input.TextChanged += OnInputTextChanged;
        }

        private void OnInputTextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(
                _placeholderVisibilityPropertyKey,
                string.IsNullOrEmpty(_input.Text) ?
                    Visibility.Visible :
                    Visibility.Hidden
            );
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
            typeof(PlaceholderInput),
            new PropertyMetadata()
        );

        #endregion

        #region PlaceholderVisibilityProperty

        public Visibility PlaceholderVisibility
        {
            get => (Visibility)GetValue(PlaceholderVisibilityProperty);
        }

        private readonly static DependencyPropertyKey _placeholderVisibilityPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(PlaceholderVisibility),
            typeof(Visibility),
            typeof(PlaceholderInput),
            new PropertyMetadata(Visibility.Visible)
        );

        public readonly static DependencyProperty PlaceholderVisibilityProperty = _placeholderVisibilityPropertyKey.DependencyProperty;

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
            typeof(PlaceholderInput),
            new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
            }
        );

        #endregion
    }
}
