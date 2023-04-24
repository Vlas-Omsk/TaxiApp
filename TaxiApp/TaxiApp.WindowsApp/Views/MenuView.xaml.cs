using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class MenuView : View
    {
        public MenuView()
        {
            InitializeComponent();

            Scope.AddDataContext<MenuViewModel>();
        }
    }
}
