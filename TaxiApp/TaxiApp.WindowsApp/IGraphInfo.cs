using TaxiApp.WindowsApp.Models.Graph;
using System.Windows;

namespace TaxiApp.WindowsApp
{
    public interface IGraphInfo
    {
        GraphType GraphType { get; }
        GraphData Data { get; }
        double Gap { get; }
        bool UseAnimation { get; }
        IGraphValue SelectedItem { get; set; }
        Rect? SelectedItemRect { get; set; }
        IGraphValue HighlitedItem { get; set; }
        Rect? HighlitedItemRect { get; set; }
        bool IsDragging { get; set; }

        void RaiseClick();
    }
}
