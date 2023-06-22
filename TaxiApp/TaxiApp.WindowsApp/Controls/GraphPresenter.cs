using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using PinkWpf;
using TaxiApp.WindowsApp.Models.Graph;

namespace TaxiApp.WindowsApp.Controls
{
    // ⠀⣞⢽⢪⢣⢣⢣⢫⡺⡵⣝⡮⣗⢷⢽⢽⢽⣮⡷⡽⣜⣜⢮⢺⣜⢷⢽⢝⡽⣝
    // ⠸⡸⠜⠕⠕⠁⢁⢇⢏⢽⢺⣪⡳⡝⣎⣏⢯⢞⡿⣟⣷⣳⢯⡷⣽⢽⢯⣳⣫⠇
    // ⠀⠀⢀⢀⢄⢬⢪⡪⡎⣆⡈⠚⠜⠕⠇⠗⠝⢕⢯⢫⣞⣯⣿⣻⡽⣏⢗⣗⠏⠀
    // ⠀⠪⡪⡪⣪⢪⢺⢸⢢⢓⢆⢤⢀⠀⠀⠀⠀⠈⢊⢞⡾⣿⡯⣏⢮⠷⠁⠀⠀
    // ⠀⠀⠀⠈⠊⠆⡃⠕⢕⢇⢇⢇⢇⢇⢏⢎⢎⢆⢄⠀⢑⣽⣿⢝⠲⠉⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠀⡿⠂⠠⠀⡇⢇⠕⢈⣀⠀⠁⠡⠣⡣⡫⣂⣿⠯⢪⠰⠂⠀⠀⠀⠀
    // ⠀⠀⠀⠀⡦⡙⡂⢀⢤⢣⠣⡈⣾⡃⠠⠄⠀⡄⢱⣌⣶⢏⢊⠂⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⢝⡲⣜⡮⡏⢎⢌⢂⠙⠢⠐⢀⢘⢵⣽⣿⡿⠁⠁⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⠨⣺⡺⡕⡕⡱⡑⡆⡕⡅⡕⡜⡼⢽⡻⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    // ⠀⠀⠀⠀⣼⣳⣫⣾⣵⣗⡵⡱⡡⢣⢑⢕⢜⢕⡝⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    //⠀⠀⠀⣴⣿⣾⣿⣿⣿⡿⡽⡑⢌⠪⡢⡣⣣⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    //⠀⠀⠀⡟⡾⣿⢿⢿⢵⣽⣾⣼⣘⢸⢸⣞⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    //⠀⠀⠀⠀⠁⠇⠡⠩⡫⢿⣝⡻⡮⣒⢽⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
    //
    // лень нормально делать
    public sealed class GraphPresenter : FrameworkElement
    {
        private static readonly Thickness _graphMargin = new Thickness(60d, 0, 0, 60d);
        private static readonly Pen _linesPen = new Pen(new SolidColorBrush(Color.FromRgb(218, 220, 221)), 1);
        private static readonly Pen _bordersPen = new Pen(new SolidColorBrush(Color.FromRgb(147, 148, 149)), 1);
        private static readonly Pen _valuePen = new Pen(new DrawingBrush()
        {
            Viewport = new Rect(0, 0, 10, 10),
            ViewportUnits = BrushMappingMode.Absolute,
            TileMode = TileMode.Tile,
            Drawing = new GeometryDrawing()
            {
                Brush = new SolidColorBrush(Color.FromRgb(147, 148, 149)),
                Geometry = new GeometryGroup()
                {
                    Children = new GeometryCollection()
                    {
                        new RectangleGeometry() { Rect = new Rect(0,0,50,50) },
                        new RectangleGeometry() { Rect = new Rect(50,50,50,50) }
                    }
                }
            }
        }, 1);
        private static readonly Thickness _linesMargin = new Thickness(-10, 0, 0, -10);
        private static readonly float _barBackgroundOpacity = 0.2f;
        private static readonly float _barBorderOpacity = 1f;
        private static readonly float _barBorderThickness = 1f;
        private List<CachedItem> _cache;
        private State _state;
        private static readonly int _delayBetweenAnimations = 100;
        private static readonly TimeSpan _animationLength = TimeSpan.FromSeconds(0.8);
        private double _partWidth;
        private double _partMinWidth;
        private bool _beforeDragging;
        private Point _startDragPoint;
        private double _startDragOffsetX;
        private static readonly Cursor _defaultCursor = Cursors.Arrow;
        private GraphType _prevGraphType;
        private const bool _showFirstLevelAtBackground = true;

        private Rect _verticalBarRectangle;
        private Rect _horizontalBarRectangle;
        private Rect _linesHorizontalRectangle;
        private Rect _linesVerticalRectangle;
        private Rect _visibleGraphRectangle;

        private double _offsetY = 0;

        private enum State
        {
            Init,
            Changed,
            Redraw
        }

        private abstract class CachedItem
        {
            public abstract bool Contains(Point point);
            public abstract ICachedItemRect GetItemFromPoint(Point point);
        }

        private class CachedBar : CachedItem, ICachedItemRect
        {
            public Rect Rect { get; }
            public Brush Background { get; }
            public Pen Border { get; }
            public TimeSpan BeginTime { get; }
            public IGraphValue Item { get; }

            public CachedBar(IGraphValue item, Rect rectangle, Brush background, Pen border, TimeSpan beginTime)
            {
                Item = item;
                Rect = rectangle;
                Background = background;
                Border = border;
                BeginTime = beginTime;
            }

            public override ICachedItemRect GetItemFromPoint(Point point)
            {
                if (Contains(point))
                    return this;
                return null;
            }

            public override bool Contains(Point point)
            {
                return Rect.Contains(point);
            }
        }

        private class CachedLine : CachedItem
        {
            public CachedPoint[] Points { get; }
            public Pen Background { get; }
            public Brush Border { get; }

            public CachedLine(CachedPoint[] points, Pen background, Brush border)
            {
                Points = points;
                Background = background;
                Border = border;
            }

            public override ICachedItemRect GetItemFromPoint(Point point)
            {
                if (Contains(point))
                    return Points.FirstOrDefault(p => p.Contains(point));

                return null;
            }

            public override bool Contains(Point point)
            {
                return Points.Any(p => p.Contains(point));
            }
        }

        private class CachedPoint : ICachedItemRect
        {
            public Point Point { get; }
            public IGraphValue Item { get; }

            Rect ICachedItemRect.Rect => new Rect(Point, new Size(1, 1));

            public CachedPoint(Point point, IGraphValue item)
            {
                Point = point;
                Item = item;
            }

            public bool Contains(Point point)
            {
                return new Rect(new Point(Point.X - 5, Point.Y - 5), new Point(Point.X + 5, Point.Y + 5)).Contains(point);
            }
        }

        private interface ICachedItemRect
        {
            IGraphValue Item { get; }
            Rect Rect { get; }
        }

        static GraphPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphPresenter), new FrameworkPropertyMetadata(typeof(GraphPresenter)));
        }

        public IGraphInfo GraphInfo { get; set; }
        public Rect GraphRectangle { get; private set; }

        public double PartWidth
        {
            get => _partWidth;
            set
            {
                _partWidth = value;
                InvalidateVisual();
            }
        }

        private bool UseAnimation => GraphInfo.UseAnimation && (_state == State.Changed || _state == State.Init);

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                var scrollData = MouseHelper.GetScrollData(e.Delta, ScrollType.Horizontal);
                if (scrollData.ScrollByPage)
                    OffsetX -= scrollData.Offset * GraphRectangle.Width;
                else
                    OffsetX -= scrollData.Offset * 10;
            }
            else
            {
                var delta = Math.Abs(e.Delta / 20d);
                if (e.Delta < 0)
                    _partWidth -= _partWidth / delta;
                else if (e.Delta > 0)
                    _partWidth += _partWidth / delta;
                if (_partWidth < _partMinWidth)
                    _partWidth = _partMinWidth;
                InvalidateVisual();
            }

            e.Handled = true;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (CaptureMouse())
            {
                _startDragPoint = e.GetPosition(this);
                _startDragOffsetX = OffsetX;
                _beforeDragging = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (_beforeDragging)
            {
                var item = GetItemFromPoint(e.GetPosition(this));
                if (GraphInfo.SelectedItem != item?.Item)
                    GraphInfo.SelectedItem = item?.Item;
                if (GraphInfo.SelectedItemRect != item?.Rect)
                    GraphInfo.SelectedItemRect = item?.Rect;
                GraphInfo.RaiseClick();
                _beforeDragging = false;
                ReleaseMouseCapture();
            }
            if (GraphInfo.IsDragging)
            {
                ReleaseMouseCapture();
                Cursor = _defaultCursor;
                GraphInfo.IsDragging = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var point = e.GetPosition(this);

            if (_beforeDragging)
            {
                var difference = _startDragPoint - point;
                if (difference.Length > 10)
                {
                    Cursor = Cursors.SizeAll;
                    _beforeDragging = false;
                    GraphInfo.IsDragging = true;
                }
            }

            if (GraphInfo.IsDragging)
            {
                var difference = _startDragPoint - point;
                OffsetX = _startDragOffsetX + difference.X;
                return;
            }

            var highlitedItem = GetItemFromPoint(point);
            if (highlitedItem != null)
                Cursor = Cursors.Hand;
            else
                Cursor = _defaultCursor;
            if (GraphInfo.HighlitedItem != highlitedItem?.Item)
                GraphInfo.HighlitedItem = highlitedItem?.Item;
            if (GraphInfo.HighlitedItemRect != highlitedItem?.Rect)
                GraphInfo.HighlitedItemRect = highlitedItem?.Rect;
        }

        private ICachedItemRect GetItemFromPoint(Point point)
        {
            return _cache?.FirstOrDefault(x => x.Contains(point))?.GetItemFromPoint(point);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            GraphRectangle = AddMargin(new Rect(new Point(0, 0), new Size(finalSize.Width, finalSize.Height)), _graphMargin);

            _partMinWidth = GraphInfo.Data == null ?
                0 :
                GraphInfo.GraphType switch
                {
                    GraphType.BarGraph => GetBarMinWidth(),
                    GraphType.Diagram => GetDiagramGapMinWidth(),
                    _ => throw new Exception()
                };
            if (_partMinWidth < 20)
                _partMinWidth = 20;
            if (_partWidth < _partMinWidth || _state == State.Changed)
                _partWidth = _partMinWidth;

            var minWidth = GraphInfo.Data == null ?
                0 :
                GraphInfo.GraphType switch
                {
                    GraphType.BarGraph => GetBarGraphWidth(),
                    GraphType.Diagram => GetDiagramWidth(),
                    _ => throw new Exception()
                };

            ViewportSize = GraphRectangle.Size;
            ScrollWidth = minWidth - GraphRectangle.Width;

            if (ScrollWidth < OffsetX)
                OffsetX = ScrollWidth;
            if (OffsetX < 0)
                OffsetX = 0;

            _verticalBarRectangle = new Rect(new Point(0, 0), new Size(_graphMargin.Left + _linesMargin.Left - 5, GraphRectangle.Height));
            _horizontalBarRectangle = new Rect(GraphRectangle.BottomLeft, new Size(GraphRectangle.Width, _graphMargin.Bottom));
            _linesHorizontalRectangle = AddMargin(GraphRectangle, new Thickness(_linesMargin.Left, 0, _linesMargin.Right, 0));
            _linesVerticalRectangle = AddMargin(GraphRectangle, new Thickness(0, _linesMargin.Top, 0, _linesMargin.Bottom));
            _visibleGraphRectangle = new Rect(
                new Point(GraphRectangle.X - OffsetX, GraphRectangle.Y - _offsetY),
                new Size(minWidth < GraphRectangle.Width ? GraphRectangle.Width : minWidth, GraphRectangle.Height)
            );

            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(_graphMargin.Left + _graphMargin.Right, _graphMargin.Top + _graphMargin.Bottom);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Background, null, new Rect(0, 0, ActualWidth, ActualHeight));

            DrawHorizontalLinesAndBorders(drawingContext);

            if (_state == State.Changed && _cache != null && _cache.Any())
            {
                switch (_prevGraphType)
                {
                    case GraphType.BarGraph:
                        HideBars(drawingContext);
                        break;
                    case GraphType.Diagram:
                        HideDiagram(drawingContext);
                        break;
                }
            }

            _cache ??= new List<CachedItem>();
            _cache.Clear();

            if (GraphInfo.Data == null)
                return;
            if (GraphInfo.Data.MaxValue <= 0)
            {
                Debug.WriteLine("Graph will not be rendered because MaxValue is less than or equal to zero");
                return;
            }

            switch (GraphInfo.GraphType)
            {
                case GraphType.BarGraph:
                    DrawBarGraph(drawingContext);
                    break;
                case GraphType.Diagram:
                    ShowDiagram(drawingContext);
                    break;
            }

            _state = State.Redraw;
            _prevGraphType = GraphInfo.GraphType;
        }

        internal void InvalidateData()
        {
            _state = State.Changed;
            InvalidateMeasure();
            InvalidateVisual();
        }

        internal void InvalidateGap()
        {
            InvalidateMeasure();
            InvalidateVisual();
        }

        internal void InvalidateGraphType()
        {
            _state = State.Changed;
            InvalidateMeasure();
            InvalidateVisual();
        }

        private void DrawText(
            DrawingContext drawingContext,
            string str,
            Rect rectangle,
            TextAlignment textAlignment,
            VerticalAlignment verticalAlignment,
            HorizontalAlignment horizontalAlignment
        )
        {
            var typeface = new Typeface(TextBlock.GetFontFamily(this), TextBlock.GetFontStyle(this), TextBlock.GetFontWeight(this), TextBlock.GetFontStretch(this));
            var formattedText = new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection, typeface, 12, Brushes.Gray, VisualTreeHelper.GetDpi(this).PixelsPerDip)
            {
                TextAlignment = textAlignment
            };

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Right:
                    rectangle.X = rectangle.Right - formattedText.Width;
                    break;
                case HorizontalAlignment.Center:
                    rectangle.X += (rectangle.Width / 2);
                    break;
                case HorizontalAlignment.Stretch:
                    formattedText.MaxTextWidth = rectangle.Width;
                    break;
            }

            switch (verticalAlignment)
            {
                case VerticalAlignment.Bottom:
                    rectangle.Y = rectangle.Bottom - formattedText.Height;
                    break;
                case VerticalAlignment.Center:
                    rectangle.Y += (rectangle.Height / 2) - (formattedText.Height / 2);
                    break;
                case VerticalAlignment.Stretch:
                    formattedText.MaxTextHeight = rectangle.Height;
                    break;
            }

            drawingContext.DrawText(formattedText, rectangle.TopLeft);
        }

        private void DrawHorizontalLinesAndBorders(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(_bordersPen, _linesHorizontalRectangle.BottomLeft, _linesHorizontalRectangle.BottomRight);
            drawingContext.DrawLine(_bordersPen, _linesVerticalRectangle.TopLeft, _linesVerticalRectangle.BottomLeft);

            var maxValue = GraphInfo.Data?.MaxValue ?? 10;

            var onePartOfMaxHeight = GraphRectangle.Height / maxValue;
            var topOffset = GraphRectangle.Top;

            for (var i = 0; i < maxValue; i++)
            {
                drawingContext.DrawLine(_linesPen, new Point(_linesHorizontalRectangle.Left, topOffset), new Point(_linesHorizontalRectangle.Right, topOffset));
                topOffset += onePartOfMaxHeight;
            }
        }

        private void DrawTextAtVerticalBar(DrawingContext drawingContext, double y, string str)
        {
            var textRect = new Rect(new Point(_verticalBarRectangle.X, y), new Size(_verticalBarRectangle.Width, 0));
            DrawText(drawingContext, str, textRect, TextAlignment.Right, VerticalAlignment.Center, HorizontalAlignment.Stretch);
        }

        private void DrawTextAtHorizontalBar(
            DrawingContext drawingContext,
            double x,
            double width,
            string str,
            double? y = null,
            double? height = null,
            TextAlignment textAlignment = TextAlignment.Center
        )
        {
            if (!y.HasValue)
                y = _horizontalBarRectangle.Y;
            if (!height.HasValue)
                height = _horizontalBarRectangle.Height;

            var textRect = new Rect(new Point(x, y.Value), new Size(width, height.Value));
            if (!Contains(_horizontalBarRectangle, textRect))
                return;

            DrawText(drawingContext, str, textRect, textAlignment, VerticalAlignment.Center, HorizontalAlignment.Stretch);
        }

        private int GetGlobalColumnsCount(IEnumerable<IGraphColumn> columns)
        {
            var columnsCount = 0;

            foreach (var column in columns)
            {
                if (column is GraphCollectionColumn innerColumns)
                    columnsCount += GetGlobalColumnsCount(innerColumns.Columns);
                else if (column is GraphValueColumn valueColumn)
                    columnsCount++;
                else
                    throw new InvalidOperationException();
            }

            if (columnsCount == 0)
                return 1;

            return columnsCount;
        }

        #region Bar Graph Renderer
        private double GetBarGraphWidth()
        {
            var columnsCount = GraphInfo.Data.Columns.Length;
            var globalColumnCount = GetGlobalColumnsCount(GraphInfo.Data.Columns);

            var result = GraphInfo.Gap * columnsCount + globalColumnCount * _partWidth;

            if (double.IsNaN(result))
                return 0;

            return result;
        }

        private double GetBarMinWidth()
        {
            var columnsCount = GraphInfo.Data.Columns.Length;
            var globalColumnCount = GetGlobalColumnsCount(GraphInfo.Data.Columns);

            return (GraphRectangle.Width - GraphInfo.Gap * columnsCount) / globalColumnCount;
        }

        private void DrawVerticalLines(DrawingContext drawingContext, Rect rectangle)
        {
            var gapHalf = GraphInfo.Gap / 2;
            var leftOffset = gapHalf + rectangle.Left;
            var columnsCount = GraphInfo.Data.Columns.Length;
            var barWidth = (rectangle.Width - GraphInfo.Gap * columnsCount) / columnsCount;

            drawingContext.PushClip(new RectangleGeometry(_linesVerticalRectangle));
            for (var i = 0; i < columnsCount - 1; i++)
            {
                leftOffset += barWidth + gapHalf;
                drawingContext.DrawLine(_linesPen, new Point(leftOffset, _linesVerticalRectangle.Top), new Point(leftOffset, _linesVerticalRectangle.Bottom));
                leftOffset += gapHalf;
            }
            drawingContext.Pop();
        }

        private void DrawBarGraph(DrawingContext drawingContext)
        {
            DrawVerticalLines(drawingContext, _visibleGraphRectangle);

            var horizontalBarRectangle = _horizontalBarRectangle;
            var textLineHeight = 0d;
            var maxDepth = GraphInfo.Data.GetMaxDepth();

            if (maxDepth != 1)
            {
                textLineHeight = _horizontalBarRectangle.Height / (maxDepth - 1);
                horizontalBarRectangle = new Rect(
                    _horizontalBarRectangle.X,
                    _horizontalBarRectangle.Y + _horizontalBarRectangle.Height,
                    _horizontalBarRectangle.Width,
                    textLineHeight
                );
            }

            ShowBars(drawingContext, _visibleGraphRectangle, horizontalBarRectangle, true, textLineHeight, GraphInfo.Gap, GraphInfo.Data.Columns);
        }

        private void HideBars(DrawingContext drawingContext)
        {
            var i = 0;
            foreach (var bar in _cache.Cast<CachedBar>())
            {
                var fromRectangle = bar.Rect;

                if (!Contains(GraphRectangle, fromRectangle))
                    continue;

                var toRectangle = new Rect(new Point(bar.Rect.X, bar.Rect.Bottom + 10), new Size(bar.Rect.Width, 0));

                var point0 = new Point(_linesHorizontalRectangle.X, fromRectangle.Y);
                var fromPoint1 = new Point(fromRectangle.X, fromRectangle.Y);
                var toPoint1 = point0;

                DrawBar(drawingContext, bar.Background, bar.Border, fromRectangle, toRectangle, point0, fromPoint1, toPoint1, true, bar.BeginTime);

                i++;
            }
        }

        private void ShowBars(
            DrawingContext drawingContext,
            Rect rectangle,
            Rect horizontalTextRectangle,
            bool isFirstLevel,
            double textLineHeight,
            double gap,
            GraphColumnCollection data
        )
        {
            var columnsCount = data.Length;
            var gapHalf = gap / 2;
            var barWidth = (rectangle.Width - gap * columnsCount) / columnsCount;
            if (barWidth < 0) barWidth = 0;
            var leftOffset = gapHalf + rectangle.Left;
            var i = 0;

            var horizontalTextRectangle2 = new Rect(
                horizontalTextRectangle.X,
                horizontalTextRectangle.Y - textLineHeight,
                horizontalTextRectangle.Width,
                horizontalTextRectangle.Height
            );

            foreach (var item in data)
            {
                var barRectangle = new Rect(leftOffset, rectangle.Top, barWidth, rectangle.Height);

                var name = item.Name?.ToString();

                if (isFirstLevel && _showFirstLevelAtBackground)
                {
                    DrawText(drawingContext, name, barRectangle, TextAlignment.Center, VerticalAlignment.Center, HorizontalAlignment.Stretch);
                }
                else
                {
                    DrawTextAtHorizontalBar(drawingContext, leftOffset, barWidth, name, horizontalTextRectangle.Y, horizontalTextRectangle.Height);
                }

                if (Contains(GraphRectangle, barRectangle))
                {
                    if (item is GraphValueColumn valueColumn)
                    {
                        ShowBar(drawingContext, barRectangle, valueColumn, i);
                    }
                    else if (item is GraphCollectionColumn columnCollection)
                    {
                        ShowBars(drawingContext, barRectangle, horizontalTextRectangle2, false, textLineHeight, 0, columnCollection.Columns);
                    }
                }

                if (i == columnsCount - 1)
                    break;

                leftOffset += barWidth + gap;
                i++;
            }
        }

        private void ShowBar(DrawingContext drawingContext, Rect rectangle, GraphValueColumn data, int index)
        {
            var color = SafeColors.GetColorFromNumber(index);
            var background = new SolidColorBrush(color) { Opacity = _barBackgroundOpacity };
            var border = new Pen(new SolidColorBrush(color) { Opacity = _barBorderOpacity }, _barBorderThickness);
            var value = data.Value?.Value ?? 0;

            var fromRectangle = new Rect(rectangle.BottomLeft, new Size(rectangle.Width, 0));
            var toRectangle = AddMargin(rectangle, new Thickness(0, rectangle.Height * ((GraphInfo.Data.MaxValue - value) / GraphInfo.Data.MaxValue), 0, 0));

            var point0 = new Point(_linesHorizontalRectangle.X, toRectangle.Y);
            var fromPoint1 = point0;
            var toPoint1 = new Point(toRectangle.X, toRectangle.Y);

            var beginTime = TimeSpan.FromMilliseconds(index * _delayBetweenAnimations);

            _cache.Add(new CachedBar(data.Value, toRectangle, background, border, beginTime));

            DrawBar(drawingContext, background, border, fromRectangle, toRectangle, point0, fromPoint1, toPoint1, UseAnimation, beginTime);
            DrawTextAtVerticalBar(drawingContext, point0.Y, value.ToString("0.00"));
        }

        private void DrawBar(DrawingContext drawingContext, Brush background, Pen border, Rect fromRectangle, Rect toRectangle, Point point0, Point fromPoint1, Point toPoint1, bool useAnimation, TimeSpan beginTime)
        {
            RectAnimation barAnimation = null;
            PointAnimation lineAnimation = null;

            if (useAnimation)
            {
                barAnimation = new RectAnimation(toRectangle, _animationLength)
                {
                    BeginTime = beginTime,
                    EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                };
                lineAnimation = new PointAnimation(toPoint1, _animationLength)
                {
                    BeginTime = beginTime,
                    EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                };
            }
            else
            {
                fromRectangle = toRectangle;
                fromPoint1 = toPoint1;
            }

            drawingContext.PushClip(new RectangleGeometry(GraphRectangle));
            drawingContext.DrawRectangle(background, border, fromRectangle, barAnimation?.CreateClock());
            //if (toRectangle.Contains(Mouse.GetPosition(this)))
            //    background.BeginAnimation(Brush.OpacityProperty, new DoubleAnimation() { To = 0.8, Duration = TimeSpan.FromSeconds(1) });
            drawingContext.Pop();

            drawingContext.PushClip(new RectangleGeometry(_linesHorizontalRectangle));
            drawingContext.DrawLine(_valuePen, point0, null, fromPoint1, lineAnimation?.CreateClock());
            drawingContext.Pop();
        }
        #endregion

        #region Diagram Renderer
        private double GetDiagramWidth()
        {
            var columnsCount = GraphInfo.Data.Columns.Length;

            return _partWidth * (columnsCount - 1);
        }

        private double GetDiagramGapMinWidth()
        {
            int columnsCount;
            double columnWidth;

            columnsCount = GetGlobalColumnsCount(GraphInfo.Data.Columns);
            if (columnsCount <= 1)
                columnWidth = GraphRectangle.Width;
            else
                columnWidth = GraphRectangle.Width / (columnsCount - 1);

            return columnWidth;
        }

        private void HideDiagram(DrawingContext drawingContext)
        {
            foreach (var line in _cache.Cast<CachedLine>())
            {
                var prevPoint = line.Points.FirstOrDefault()?.Point;

                var i = 0;
                foreach (var point in line.Points.Select(x => x.Point))
                {
                    var fromPoint0 = prevPoint;
                    var toPoint0 = point;
                    var beginTime = _animationLength.Subtract(TimeSpan.FromMilliseconds(200)) * i;

                    if (prevPoint.HasValue)
                    {
                        var lineAnimation = new PointAnimation(toPoint0, _animationLength)
                        {
                            BeginTime = beginTime,
                            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                        };

                        drawingContext.PushClip(new RectangleGeometry(GraphRectangle));
                        drawingContext.DrawLine(line.Background, fromPoint0.Value, lineAnimation?.CreateClock(), point, null);
                        drawingContext.Pop();
                    }

                    var radiusAnimation = new DoubleAnimation(0, _animationLength)
                    {
                        BeginTime = beginTime,
                        EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                    };

                    drawingContext.PushClip(new RectangleGeometry(GraphRectangle));
                    drawingContext.DrawEllipse(line.Border, null, point, null, 4, radiusAnimation?.CreateClock(), 4, radiusAnimation?.CreateClock());
                    drawingContext.Pop();

                    prevPoint = point;
                    i++;
                }
            }
        }

        private void ShowDiagram(DrawingContext drawingContext)
        {
            int columnsCount;
            double columnWidth;

            if (GraphInfo.Data.GetMaxDepth() == 1)
            {
                columnsCount = GraphInfo.Data.Columns.Length;
                if (columnsCount <= 1)
                    columnWidth = _visibleGraphRectangle.Width;
                else
                    columnWidth = _visibleGraphRectangle.Width / (columnsCount - 1);
                if (columnWidth < _partWidth) columnWidth = _partWidth;

                ShowPoints(drawingContext, _visibleGraphRectangle, GraphInfo.Data.Columns, columnWidth, null, 0);

                DrawHorizontalTexts(drawingContext, columnWidth, GraphInfo.Data.Columns);
            }
            else
            {
                columnsCount = GraphInfo.Data.Columns.Max(x => ((GraphCollectionColumn)x).Columns.Length);
                if (columnsCount <= 1)
                    columnWidth = _visibleGraphRectangle.Width;
                else
                    columnWidth = _visibleGraphRectangle.Width / (columnsCount - 1);
                if (columnWidth < _partWidth) columnWidth = _partWidth;

                var i = 0;
                foreach (var item in GraphInfo.Data.Columns)
                {
                    ShowPoints(drawingContext, _visibleGraphRectangle, ((GraphCollectionColumn)item).Columns, columnWidth, item.Name.ToString(), i);
                    i++;
                }

                GraphColumnCollection data = null;
                foreach (var item in GraphInfo.Data.Columns.Cast<GraphCollectionColumn>())
                    if (data == null || item.Columns.Length > data.Length)
                        data = item.Columns;

                DrawHorizontalTexts(drawingContext, columnWidth, data);
            }
        }

        private void DrawHorizontalTexts(DrawingContext drawingContext, double columnWidth, GraphColumnCollection data)
        {
            var leftOffset = _visibleGraphRectangle.X;
            var length = data.Length;

            var i = 0;
            foreach (var item in data)
            {
                if (i == length - 2)
                    columnWidth /= 2;

                DrawTextAtHorizontalBar(
                    drawingContext,
                    leftOffset,
                    columnWidth,
                    item.Name?.ToString(),
                    textAlignment: i == length - 1 ? TextAlignment.Right : TextAlignment.Left
                );

                leftOffset += columnWidth;
                i++;
            }
        }

        private void ShowPoints(DrawingContext drawingContext, Rect rectangle, GraphColumnCollection data, double columnWidth, string name, int index)
        {
            var color = SafeColors.GetColorFromNumber(index);
            var background = new Pen(new SolidColorBrush(color) { Opacity = _barBackgroundOpacity }, 3);
            var border = new SolidColorBrush(color) { Opacity = _barBorderOpacity };

            Point? prevPoint = null;
            var leftOffset = rectangle.Left;
            var isValueDrawed = name == null;
            var points = new List<CachedPoint>();

            var i = 0;
            foreach (var item in data.Cast<GraphValueColumn>())
            {
                var value = item.Value?.Value ?? 0;
                var point = new Point(leftOffset, rectangle.Height * ((GraphInfo.Data.MaxValue - value) / GraphInfo.Data.MaxValue));
                var isPointVisible = Contains(GraphRectangle, new Point(point.X, point.Y));
                var beginTime = _animationLength.Subtract(TimeSpan.FromMilliseconds(200)) * (i - 1);

                if (prevPoint.HasValue)
                {
                    if (Contains(GraphRectangle, new Rect(prevPoint.Value, point)))
                    {
                        var fromPoint1 = prevPoint.Value;
                        var toPoint1 = point;

                        PointAnimation lineAnimation = null;

                        if (UseAnimation)
                        {
                            lineAnimation = new PointAnimation(toPoint1, _animationLength)
                            {
                                BeginTime = beginTime,
                                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                            };
                        }
                        else
                        {
                            fromPoint1 = toPoint1;
                        }

                        drawingContext.PushClip(new RectangleGeometry(GraphRectangle));
                        drawingContext.DrawLine(background, prevPoint.Value, null, fromPoint1, lineAnimation?.CreateClock());
                        drawingContext.Pop();

                        points.Add(new CachedPoint(point, item.Value));
                    }
                }
                else
                {
                    points.Add(new CachedPoint(point, item.Value));
                }

                if (isPointVisible)
                {
                    var fromRadius = 0;
                    var toRadius = 4;

                    DoubleAnimation radiusAnimation = null;

                    if (UseAnimation)
                    {
                        radiusAnimation = new DoubleAnimation(toRadius, _animationLength)
                        {
                            BeginTime = beginTime,
                            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
                        };
                    }
                    else
                    {
                        fromRadius = toRadius;
                    }

                    drawingContext.PushClip(new RectangleGeometry(GraphRectangle));
                    drawingContext.DrawEllipse(border, null, point, null, fromRadius, radiusAnimation?.CreateClock(), fromRadius, radiusAnimation?.CreateClock());
                    drawingContext.Pop();

                    var point0 = new Point(_linesHorizontalRectangle.X, point.Y);

                    if (!isValueDrawed)
                    {
                        drawingContext.PushClip(new RectangleGeometry(_linesHorizontalRectangle));
                        drawingContext.DrawLine(_valuePen, point0, null, point, null);
                        drawingContext.Pop();

                        DrawTextAtVerticalBar(drawingContext, point0.Y, name);

                        isValueDrawed = true;
                    }
                }

                prevPoint = point;
                leftOffset += columnWidth;
                i++;
            }

            _cache.Add(new CachedLine(points.ToArray(), background, border));
        }
        #endregion

        private static bool Contains(Rect rectangle, Point point)
        {
            return 
                point.X >= rectangle.Left && 
                point.X <= rectangle.Right && 
                point.Y >= rectangle.Top && 
                point.Y <= rectangle.Bottom;
        }

        private static bool Contains(Rect rectangle1, Rect rectangle2)
        {
            return 
                Contains(rectangle1, rectangle2.TopLeft) || 
                Contains(rectangle1, rectangle2.TopRight) || 
                Contains(rectangle1, rectangle2.BottomLeft) || 
                Contains(rectangle1, rectangle2.BottomRight);
        }

        private static Rect AddMargin(Rect rectangle, Thickness thickness)
        {
            var width = rectangle.Width - thickness.Right - thickness.Left;
            if (width < 0)
                width = 0;
            var height = rectangle.Height - thickness.Bottom - thickness.Top;
            if (height < 0)
                height = 0;
            return new Rect(rectangle.Left + thickness.Left, rectangle.Top + thickness.Top, width, height);
        }

        #region BackgroundProperty
        public Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public readonly static DependencyProperty BackgroundProperty = DependencyProperty.Register(
            nameof(Background),
            typeof(Brush),
            typeof(GraphPresenter),
            new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender)
        );
        #endregion

        #region OffsetXProperty
        public double OffsetX
        {
            get => (double)GetValue(OffsetXProperty);
            set => SetValue(OffsetXProperty, value);
        }

        public readonly static DependencyProperty OffsetXProperty = DependencyProperty.Register(
            nameof(OffsetX),
            typeof(double),
            typeof(GraphPresenter),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender)
        );
        #endregion

        #region ScrollWidthProperty
        public double ScrollWidth
        {
            get => (double)GetValue(ScrollWidthProperty);
            private set => SetValue(_scrollWidthPropertyKey, value);
        }

        private readonly static DependencyPropertyKey _scrollWidthPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(ScrollWidth),
            typeof(double),
            typeof(GraphPresenter),
            new FrameworkPropertyMetadata(0d)
        );

        public readonly static DependencyProperty ScrollWidthProperty =
            _scrollWidthPropertyKey.DependencyProperty;
        #endregion

        #region ViewportSizeProperty
        public Size ViewportSize
        {
            get => (Size)GetValue(ViewportSizeProperty);
            private set => SetValue(_viewportSizePropertyKey, value);
        }

        private readonly static DependencyPropertyKey _viewportSizePropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(ViewportSize),
            typeof(Size),
            typeof(GraphPresenter),
            new FrameworkPropertyMetadata(new Size(0, 0))
        );

        public readonly static DependencyProperty ViewportSizeProperty =
            _viewportSizePropertyKey.DependencyProperty;
        #endregion
    }
}
