using CommunityToolkit.Mvvm.ComponentModel;
using PinkWpf;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class TariffsViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoadingState _loadingState;
    }
}
