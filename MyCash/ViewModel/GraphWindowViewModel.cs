using GalaSoft.MvvmLight.Command;
using LiveCharts;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MyCash.ViewModel
{
    public class GraphWindowViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel = new MainViewModel();

        public List<string> Labels { get; set; } = MediatorClass.LabelsForColumn;
        public SeriesCollection ColumnSeries { get; set; } = MediatorClass.ColumnSeries;

        public SeriesCollection PieSeries { get; set; } = MediatorClass.PieSeries;

        public GraphWindowViewModel() { }

        public ICommand CloseWindow => new RelayCommand(() =>
        {
            MediatorClass.LabelsForColumn.Clear();
            MediatorClass.DataForColumn.Clear();
            MediatorClass.ColumnSeries[0].Values.Clear();
            MediatorClass.PieSeries.Clear();

            // получения количества окон и закрытие последнего
            Application.Current.Windows[1].Close();// = ShutdownMode.OnMainWindowClose;
        });

    }
}
