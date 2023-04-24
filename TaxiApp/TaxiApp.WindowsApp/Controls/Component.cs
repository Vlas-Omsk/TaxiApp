using System.Windows.Controls;

namespace TaxiApp.WindowsApp.Controls
{
    public class Component : UserControl
    {
        public Component()
        {
            Scope = new ElementScope(this);
        }

        protected ElementScope Scope { get; }
    }
}
