using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class OrdersView : View
    {
        public OrdersView()
        {
            InitializeComponent();

            Scope.AddDataContext<OrdersViewModel>();
        }
    }
}
