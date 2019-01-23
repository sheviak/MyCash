using System.Windows;

namespace MyCash
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
