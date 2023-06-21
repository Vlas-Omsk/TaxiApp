using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class TariffView : View
    {
        public TariffView(int id)
        {
            InitializeComponent();

            Scope.AddDataContext<TariffViewModel>(x => x.Init(id));
        }
    }
}
