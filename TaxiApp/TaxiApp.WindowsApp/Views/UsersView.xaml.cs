using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class UsersView : View
    {
        public UsersView()
        {
            InitializeComponent();

            Scope.AddDataContext<UsersViewModel>();
        }
    }
}
