using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class UserView : View
    {
        public UserView(string login)
        {
            InitializeComponent();

            Scope.AddDataContext<UserViewModel>(x => x.Init(login));
        }
    }
}
