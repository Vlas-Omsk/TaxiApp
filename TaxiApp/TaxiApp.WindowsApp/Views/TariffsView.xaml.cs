using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class TariffsView : View
    {
        public TariffsView()
        {
            InitializeComponent();

            Scope.AddDataContext<TariffsViewModel>();
        }
    }
}
