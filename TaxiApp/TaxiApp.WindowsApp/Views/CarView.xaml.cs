using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class CarView : View
    {
        public CarView(int id)
        {
            InitializeComponent();

            Scope.AddDataContext<CarViewModel>(x => x.Init(id));
        }
    }
}
