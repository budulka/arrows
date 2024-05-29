

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;



namespace arrows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Fureimu.NavigationService.Navigate(new Menu());
        }
    }
}
