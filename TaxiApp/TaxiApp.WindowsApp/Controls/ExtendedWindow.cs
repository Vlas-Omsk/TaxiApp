using System.Windows;

namespace TaxiApp.WindowsApp.Controls
{
    public class ExtendedWindow : Window
    {
        public ExtendedWindow()
        {
            Scope = new ElementScope(this);
        }

        protected ElementScope Scope { get; }
    }
}
