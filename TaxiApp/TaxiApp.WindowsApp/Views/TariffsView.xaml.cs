using System;
using System.ComponentModel;
using System.Linq;
using TaxiApp.WindowsApp.Controls;
using TaxiApp.WindowsApp.ViewModels;

namespace TaxiApp.WindowsApp.Views
{
    public partial class TariffsView : View
    {
        private const int _columnsCount = 2;

        public TariffsView()
        {
            InitializeComponent();

            Scope.AddDataContext<TariffsViewModel>();

            var dataContext = (TariffsViewModel)DataContext;

            dataContext.PropertyChanged += OnDataContextPropertyChanged;
        }

        ~TariffsView()
        {
            var dataContext = (TariffsViewModel)DataContext;

            dataContext.PropertyChanged -= OnDataContextPropertyChanged;
        }

        private void OnDataContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TariffsViewModel.Tariffs))
                UpdateTable();
        }

        private void UpdateTable()
        {
            var dataContext = (TariffsViewModel)DataContext;
            var rowsCount = dataContext.Tariffs == null ?
                0 :
                (int)Math.Ceiling(dataContext.Tariffs.Length / (double)_columnsCount);

            TariffsGrid.Rows = null;
            TariffsGrid.Children.Clear();

            if (rowsCount == 0)
                return;

            TariffsGrid.Rows = string.Join(
                ",[{Gap}],",
                Enumerable.Repeat("{RowHeight}", rowsCount)
            );

            foreach (var tariff in dataContext.Tariffs)
                TariffsGrid.Children.Add(new TariffButton()
                {
                    Title = tariff.Name,
                    Command = dataContext.OpenCommand,
                    CommandParameter = tariff
                });
        }
    }
}
