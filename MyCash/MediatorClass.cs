using System.Collections.Generic;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;

namespace MyCash
{
    public class MediatorClass
    {
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
