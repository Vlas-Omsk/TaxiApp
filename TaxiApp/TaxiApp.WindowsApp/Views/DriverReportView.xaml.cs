using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class DriverReportView : View
    {
        public DriverReportView(int id)
        {
            InitializeComponent();

            Scope.AddDataContext<DriverReportViewModel>(x => x.Init(id));
        }
    }
}
