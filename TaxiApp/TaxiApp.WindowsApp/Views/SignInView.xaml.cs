using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class SignInView : View
    {
        public SignInView()
        {
            InitializeComponent();

            Scope.AddDataContext<SignInViewModel>();
        }
    }
}
