using System.Windows.Controls;
using System.Windows.Input;

namespace TaxiApp.WindowsApp.Models
{
    public sealed class MenuItemModel
    {
        public MenuItemModel(UserControl image, string name, ICommand command)
        {
            Image = image;
            Name = name;
            Command = command;
        }

        public UserControl Image { get; }
        public string Name { get; }
        public ICommand Command { get; }
    }
}
