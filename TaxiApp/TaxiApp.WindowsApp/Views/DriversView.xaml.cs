using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class DriversView : View
    {
        public DriversView(bool showOnlyActive)
        {
            InitializeComponent();

            Scope.AddDataContext<DriversViewModel>(x => x.Init(showOnlyActive));
        }
    }
}
