using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PinkWpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.WindowsApp.Models.Graph;
using TaxiApp.WindowsApp.Services;

namespace TaxiApp.WindowsApp.ViewModels
{
    [ViewModel]
    internal sealed partial class DriverReportViewModel : ObservableObject
    {
        private readonly ApiService _apiService;
        private int _id;

        public DriverReportViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public void Init(int id)
        {
            _id = id;
        }

        [ObservableProperty]
        private DateTime _periodBegin = DateTime.Now.Date;

        [ObservableProperty]
        private DateTime _periodEnd = DateTime.Now.Date.AddDays(1);

        [ObservableProperty]
        private GraphData _data;

        [ObservableProperty]
        private string _loadingStatus;

        [ObservableProperty]
        private LoadingState _loadingState = LoadingState.Loaded;

        [RelayCommand]
        private async Task Create()
        {
            if (PeriodBegin.Date >= PeriodEnd.Date)
            {
                MessageBox.Show("Конец периода не может быть меньше начала");

                return;
            }

            LoadingState = LoadingState.Loading;

            var response = await _apiService.Send(new GetReportForDriverOrdersQuery(
                _id,
                PeriodBegin.ToUniversalTime(),
                PeriodEnd.ToUniversalTime()
            ));

            if (!response.Success)
            {
                LoadingStatus = response.Message;
                LoadingState = LoadingState.Failed;
                return;
            }

            Data = GraphData.FromArray(
                response.Value,
                x => new GraphValue((double)(x.Cost / 1000)),
                x => $"#{x.Id}, {x.Cost}"
            );

            LoadingState = LoadingState.Loaded;
        }
    }
}
