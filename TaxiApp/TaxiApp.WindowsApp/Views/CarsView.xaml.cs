using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class CarsView : View
    {
        public CarsView()
        {
            InitializeComponent();

            Scope.AddDataContext<CarsViewModel>();
        }
    }
}
