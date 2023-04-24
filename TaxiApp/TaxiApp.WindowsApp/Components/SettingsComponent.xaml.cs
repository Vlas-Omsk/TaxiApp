using TaxiApp.WindowsApp.ComponentModels;
using TaxiApp.WindowsApp.Controls;

namespace TaxiApp.WindowsApp.Components
{
    public partial class SettingsComponent : Component
    {
        public SettingsComponent()
        {
            InitializeComponent();

            Scope.AddDataContext<SettingsComponentModel>();
        }
    }
}
