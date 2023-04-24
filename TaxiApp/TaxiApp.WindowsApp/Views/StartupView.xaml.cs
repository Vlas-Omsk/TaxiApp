using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class StartupView : View
    {
        public StartupView()
        {
            InitializeComponent();

            Scope.AddDataContext<StartupViewModel>();
        }
    }
}
