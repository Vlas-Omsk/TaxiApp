using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    public class View : UserControl
    {
        public View()
        {
            Scope = new ElementScope(this);
        }

        protected ElementScope Scope { get; }
    }
}
