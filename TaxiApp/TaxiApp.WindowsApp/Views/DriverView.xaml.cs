using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class DriverView : View
    {
        public DriverView(int id)
        {
            InitializeComponent();

            Scope.AddDataContext<DriverViewModel>(x => x.Init(id));
        }
    }
}
