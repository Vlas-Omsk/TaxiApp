using System.Windows;
using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    public class InfoTile : ContentControl
    {
        static InfoTile()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoTile), new FrameworkPropertyMetadata(typeof(InfoTile)));
        }
    }
}
