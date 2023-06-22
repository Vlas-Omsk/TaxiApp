using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using TaxiApp.WindowsApp.Models.Graph;

namespace TaxiApp.WindowsApp.Controls
{
    [TemplatePart(Name = "PART_Presenter", Type = typeof(GraphPresenter))]
    [DefaultEvent("Click")]
    public class Graph : ContentControl, IGraphInfo, ICommandSource
    {
        public GraphPresenter GraphPresenter { get; private set; }

        static Graph()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Graph), new FrameworkPropertyMetadata(typeof(Graph)));
        }

        public override void OnApplyTemplate()
        {
            GraphPresenter = (GraphPresenter)GetTemplateChild("PART_Presenter");
            GraphPresenter.GraphInfo = this;
        }

        void IGraphInfo.RaiseClick()
        {
            RoutedEventArgs clickEvent = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(clickEvent);

            if (Command != null && Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);
        }

        #region ICommandSource

        #region CommandProperty
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(Graph),
            new FrameworkPropertyMetadata(null)
        );
        #endregion

        #region CommandParameterProperty
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            nameof(CommandParameter),
            typeof(object),
            typeof(Graph),
            new FrameworkPropertyMetadata((object)null)
        );
        #endregion

        #region CommandTarget
        public IInputElement CommandTarget
        {
            get => (IInputElement)GetValue(CommandTargetProperty);
            set => SetValue(CommandTargetProperty, value);
        }

        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(
            nameof(CommandTarget),
            typeof(IInputElement),
            typeof(Graph),
            new FrameworkPropertyMetadata((IInputElement)null)
        );
        #endregion

        #endregion

        #region DataProperty
        public GraphData Data
        {
            get => (GraphData)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public readonly static DependencyProperty DataProperty = DependencyProperty.Register(
            nameof(Data),
            typeof(GraphData),
            typeof(Graph),
            new FrameworkPropertyMetadata(null, OnDataChanged)
        );

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Graph)d).GraphPresenter?.InvalidateData();
        }
        #endregion

        #region GapProperty
        public double Gap
        {
            get => (double)GetValue(GapProperty);
            set => SetValue(GapProperty, value);
        }

        public readonly static DependencyProperty GapProperty = DependencyProperty.Register(
            nameof(Gap),
            typeof(double),
            typeof(Graph),
            new FrameworkPropertyMetadata(20d, OnGapChanged)
        );

        private static void OnGapChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Graph)d).GraphPresenter?.InvalidateGap();
        }
        #endregion

        #region SelectedItemProperty
        public IGraphValue SelectedItem
        {
            get => (IGraphValue)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        IGraphValue IGraphInfo.SelectedItem
        {
            get => SelectedItem;
            set => SetCurrentValue(SelectedItemProperty, value);
        }

        public readonly static DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(IGraphValue),
            typeof(Graph),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        #endregion

        #region SelectedItemRectProperty
        public Rect? SelectedItemRect
        {
            get => (Rect?)GetValue(SelectedItemRectProperty);
            set => SetValue(SelectedItemRectProperty, value);
        }

        Rect? IGraphInfo.SelectedItemRect
        {
            get => SelectedItemRect;
            set => SetCurrentValue(SelectedItemRectProperty, value);
        }

        public readonly static DependencyProperty SelectedItemRectProperty = DependencyProperty.Register(
            nameof(SelectedItemRect),
            typeof(Rect?),
            typeof(Graph),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        #endregion

        #region HighlitedItemProperty
        public IGraphValue HighlitedItem
        {
            get => (IGraphValue)GetValue(HighlitedItemProperty);
            set => SetValue(HighlitedItemProperty, value);
        }

        IGraphValue IGraphInfo.HighlitedItem
        {
            get => HighlitedItem;
            set => SetCurrentValue(HighlitedItemProperty, value);
        }

        public readonly static DependencyProperty HighlitedItemProperty = DependencyProperty.Register(
            nameof(HighlitedItem),
            typeof(IGraphValue),
            typeof(Graph),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        #endregion

        #region HighlitedItemRectProperty
        public Rect? HighlitedItemRect
        {
            get => (Rect?)GetValue(HighlitedItemRectProperty);
            set => SetValue(HighlitedItemRectProperty, value);
        }

        Rect? IGraphInfo.HighlitedItemRect
        {
            get => HighlitedItemRect;
            set => SetCurrentValue(HighlitedItemRectProperty, value);
        }

        public readonly static DependencyProperty HighlitedItemRectProperty = DependencyProperty.Register(
            nameof(HighlitedItemRect),
            typeof(Rect?),
            typeof(Graph),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        #endregion

        #region IsDraggingProperty
        public bool IsDragging
        {
            get => (bool)GetValue(IsDraggingProperty);
        }

        bool IGraphInfo.IsDragging
        {
            get => IsDragging;
            set => SetValue(_isDraggingPropertyKey, value);
        }

        private readonly static DependencyPropertyKey _isDraggingPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(IsDragging),
            typeof(bool),
            typeof(Graph),
            new FrameworkPropertyMetadata(false)
        );

        public readonly static DependencyProperty IsDraggingProperty = _isDraggingPropertyKey.DependencyProperty;
        #endregion

        #region GraphTypeProperty
        public GraphType GraphType
        {
            get => (GraphType)GetValue(GraphTypeProperty);
            set => SetValue(GraphTypeProperty, value);
        }

        public readonly static DependencyProperty GraphTypeProperty = DependencyProperty.Register(
            nameof(GraphType),
            typeof(GraphType),
            typeof(Graph),
            new FrameworkPropertyMetadata(GraphType.BarGraph, OnGraphTypeChanged)
        );

        private static void OnGraphTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Graph)d).GraphPresenter?.InvalidateGraphType();
        }
        #endregion

        #region UseAnimationProperty
        public bool UseAnimation
        {
            get => (bool)GetValue(UseAnimationProperty);
            set => SetValue(UseAnimationProperty, value);
        }

        public readonly static DependencyProperty UseAnimationProperty = DependencyProperty.Register(
            nameof(UseAnimation),
            typeof(bool),
            typeof(Graph),
            new FrameworkPropertyMetadata(true)
        );
        #endregion

        #region ClickEvent
        [Category("Behavior")]
        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(
            nameof(Click),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(GraphPresenter)
        );
        #endregion
    }
}
