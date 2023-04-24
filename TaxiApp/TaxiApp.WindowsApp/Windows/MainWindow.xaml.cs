using System.Windows;
using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.WindowModels;

namespace TaxiApp.WindowsApp.Windows
{
    public partial class MainWindow : ExtendedWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Scope.AddDataContext<MainWindowModel>();
        }
    }
}
