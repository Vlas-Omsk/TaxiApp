using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.WindowsApp.Models;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class CarViewModel : ObservableObject
    {
        private int _id;

        public void Init(int id)
        {
            _id = id;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(EditButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(SaveButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(EditPhotoButtonVisibility))]
        private bool _isReadOnly = true;

        public Visibility EditButtonVisibility => IsReadOnly ? Visibility.Visible : Visibility.Collapsed;
        public Visibility SaveButtonVisibility => IsReadOnly ? Visibility.Collapsed : Visibility.Visible;
        public Visibility EditPhotoButtonVisibility => IsReadOnly ? Visibility.Hidden : Visibility.Visible;

        [ObservableProperty]
        private string _govermentNumber;

        [ObservableProperty]
        private string _model;

        [ObservableProperty]
        private string _color;

        [ObservableProperty]
        private string _additionalInfo;

        [ObservableProperty]
        private TariffModel[] _tariffs;

        [ObservableProperty]
        private int _yearOfManufacture;

        [ObservableProperty]
        private LoadingState _loadingState;

        [ObservableProperty]
        private string _loadingStatus;

        [RelayCommand]
        private async Task Load()
        {
            LoadingState = LoadingState.Loading;



            LoadingState = LoadingState.Loaded;
        }

        [RelayCommand]
        private void Edit()
        {
            IsReadOnly = false;
        }

        [RelayCommand]
        private async Task Save()
        {
            IsReadOnly = true;
        }

        [RelayCommand]
        private void EditPhoto()
        {

        }
    }
}
