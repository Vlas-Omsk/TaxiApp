using TaxiApp.WindowsApp.ComponentModels;
using TaxiApp.WindowsApp.Controls;

namespace TaxiApp.WindowsApp.Components
{
    public partial class NavigationComponent : Component
    {
        public NavigationComponent()
        {
            InitializeComponent();

            Scope.AddDataContext<NavigationComponentModel>();
        }
    }
}
