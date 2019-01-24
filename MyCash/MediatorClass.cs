using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace MyCash
{
    public class MediatorClass
    {
        /// <summary>
        /// Коллекция для содержания списка предметов
        /// </summary>
        public static ObservableCollection<string> Lessons { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Подписи для столбчатой диаграммы
        /// </summary>
        public static List<string> LabelsForColumn = new List<string>();

        /// <summary>
        /// Данные для столбчатой диаграммы
        /// </summary>
        public static ChartValues<int> DataForColumn = new ChartValues<int>();

        /// <summary>
        /// Серия для столбчатой диаграммы
        /// </summary>
        public static SeriesCollection ColumnSeries = new SeriesCollection
        {
            new ColumnSeries
            {
                Values = new ChartValues<int>(), //MediatorClass.DataForColumn, // тут значения для колонок
                DataLabels = true,
                Title = "Заработок",
                LabelPoint = point => point.Y + " грн." ,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                FontSize = 12
            }
        };

        /// <summary>
        /// Серии для круговой диаграммы (включает название и количество)
        /// </summary>
        public static SeriesCollection PieSeries = new SeriesCollection();
    }
}
