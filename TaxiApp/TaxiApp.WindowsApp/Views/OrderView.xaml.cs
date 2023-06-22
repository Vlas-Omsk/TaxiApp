using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class OrderView : View
    {
        public OrderView(EditType editType, int? id = null)
        {
            InitializeComponent();

            Scope.AddDataContext<OrderViewModel>(x => x.Init(editType, id));
        }
    }
}
